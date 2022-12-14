using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    private Inventory _inventory;
    private PlayerInfo _playerInfo;
    private ExpProgressBar _expProgressBar;
    private HealthAndManaProgressBar _healthAndManaProgressBar;

    void Start()
    {
        _inventory = transform.Find("InGame").Find("Inventory").GetComponent<Inventory>();
        _expProgressBar = GameObject.Find("Canvas").GetComponent<ExpProgressBar>();
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
        _healthAndManaProgressBar = GameObject.Find("Canvas").GetComponent<HealthAndManaProgressBar>();
    }

    public IEnumerator GetIDCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", _playerInfo.Username);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getUserID.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            _playerInfo.ID = req.downloadHandler.text;
            Debug.Log("User ID successfully logged -> " + _playerInfo.ID.ToString());
            _inventory.CreateGridArray();
            _inventory.LoadInventory();
            _playerInfo.GetExperience();
            _playerInfo.GetHealthMana();
        }
        else
        {
            Debug.Log("User login failed: # " + req.downloadHandler.text);
        }
    }

    public IEnumerator GetExperienceCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getUserXP.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            var result = int.TryParse(req.downloadHandler.text, out var number);
            if(result)
            {
                _playerInfo.Experience = number;
            }
            Debug.Log("User XP successfully logged -> " + req.downloadHandler.text);
            _playerInfo.GetLevel();
        }
        else
        {
            Debug.Log("User Exp failed: # " + req.downloadHandler.text);
        }
    }

    public IEnumerator GetLevelCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getUserLVL.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            var result = int.TryParse(req.downloadHandler.text, out var number);
            if(result)
            {
                _playerInfo.Level = number;
                _playerInfo.LoadLevelToUI();
            }
            Debug.Log("User LVL successfully logged -> " + req.downloadHandler.text);
            _expProgressBar.UpdateProgressBar();
        }
        else
        {
            Debug.Log("User LVL failed: # " + req.downloadHandler.text);
        }
    }

    public IEnumerator GetHealthManaCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getHealthMana.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            string[] resultOfQuery = new string[4];
            resultOfQuery = req.downloadHandler.text.Split('/');
            var result = int.TryParse(resultOfQuery[0], out var number);
            if(result)
            {
                _playerInfo.Health = number;
            }
            result = int.TryParse(resultOfQuery[1], out number);
            if(result)
            {
                _playerInfo.Mana = number;
            }
            result = int.TryParse(resultOfQuery[2], out number);
            if(result)
            {
                _playerInfo.MaxHealth = number;
            }
            result = int.TryParse(resultOfQuery[3], out number);
            if(result)
            {
                _playerInfo.MaxMana = number;
            }
            _healthAndManaProgressBar.UpdateProgressBar();
            Debug.Log("User LVL successfully logged -> " + req.downloadHandler.text);
        }
        else
        {
            Debug.Log("User LVL failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateLevelCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewLevel", _playerInfo.Level);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UserLvlUp.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User LVL UP successfully");
        }
        else
        {
            Debug.Log("User LVL failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateExperienceCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewXP", _playerInfo.Experience);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UserXpUpdate.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User XP Update successfully");
        }
        else
        {
            Debug.Log("User XP Update failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateHealthAndManaCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewHealth", _playerInfo.Health);
        form.AddField("NewMana", _playerInfo.Mana);
        
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UserHealthManaUpdate.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User HealthMana Update successfully");
        }
        else
        {
            Debug.Log("User HealthMana Update failed: # " + req.downloadHandler.text);
        }
    }
}
