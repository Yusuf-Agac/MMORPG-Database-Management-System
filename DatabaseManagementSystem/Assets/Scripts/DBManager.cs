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
    private ChangeProfileImage _changeProfileImage;
    private FriendList _friendList;
    private SkillPoint _skillPoint;
    private SkillList _skillList;
    private SkillBar _skillBar;
    private Coin _coin;

    void Start()
    {
        _inventory = GetComponent<Inventory>();
        _expProgressBar = GetComponent<ExpProgressBar>();
        _playerInfo = GetComponent<PlayerInfo>();
        _healthAndManaProgressBar = GetComponent<HealthAndManaProgressBar>();
        _changeProfileImage = GetComponent<ChangeProfileImage>();
        _friendList = GetComponent<FriendList>();
        _skillPoint = GetComponent<SkillPoint>();
        _skillList = GetComponent<SkillList>();
        _skillBar = GetComponent<SkillBar>();
        _coin = GetComponent<Coin>();
    }

    public IEnumerator GetIDCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/id.php?Username=" + _playerInfo.Username);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            _playerInfo.ID = req.downloadHandler.text;
            Debug.Log("User ID successfully logged -> " + _playerInfo.ID.ToString());
            _inventory.CreateGridArray();
            _inventory.LoadInventory();
            _playerInfo.GetExperience();
            _playerInfo.GetHealthMana();
            _playerInfo.GetProfilePicture();
            _friendList.LoadFriendList();
            _playerInfo.GetSkillPoint();
            _skillList.LoadSkills();
            _skillBar.LoadSkillBar();
            _playerInfo.GetCoin();
            _coin.LoadCoin();
        }
        else
        {
            Debug.LogWarning("User login failed: # " + req.downloadHandler.text);
        }
    }

    public IEnumerator GetExperienceCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/experience.php?ID=" + _playerInfo.ID);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            var result = int.TryParse(req.downloadHandler.text, out var number);
            if(result)
            {
                _playerInfo.Experience = number;
            }
            Debug.Log("User XP successfully receipt -> " + req.downloadHandler.text);
            _playerInfo.GetLevel();
        }
        else
        {
            Debug.LogWarning("User XP receipt failed: # " + req.downloadHandler.text);
        }
    }

    public IEnumerator GetLevelCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/level.php?ID=" + _playerInfo.ID);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            var result = int.TryParse(req.downloadHandler.text, out var number);
            if(result)
            {
                _playerInfo.Level = number;
                _playerInfo.LoadLevelToUI();
            }
            Debug.Log("User LVL successfully receipt -> " + req.downloadHandler.text);
            _expProgressBar.UpdateProgressBar();
        }
        else
        {
            Debug.LogWarning("User LVL receipt failed: # " + req.downloadHandler.text);
        }
    }

    public IEnumerator GetHealthManaCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/health-mana.php?ID=" + _playerInfo.ID);
        
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
            Debug.Log("User HealthMana successfully receipt -> " + req.downloadHandler.text);
        }
        else
        {
            Debug.LogWarning("User HealthMana receipt failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator GetProfilePictureCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/profile-picture.php?ID=" + _playerInfo.ID);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            _playerInfo.ProfilePicture = req.downloadHandler.text;
            _changeProfileImage.UpdateProfilePicture();
            Debug.Log("User ProfilePicture successfully receipt -> " + _playerInfo.ID.ToString());
        }
        else
        {
            Debug.LogWarning("User ProfilePicture receipt failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator GetSkillPointCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/skill-point.php?ID=" + _playerInfo.ID);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            var result = int.TryParse(req.downloadHandler.text, out var number);
            if(result)
            {
                _playerInfo.SkillPoint = number;
            }
            Debug.Log("User SkillPoint successfully receipt -> " + req.downloadHandler.text);
            _skillPoint.LoadSkillPoint();
        }
        else
        {
            Debug.LogWarning("User SkillPoint receipt failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator GetCoinCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/coin.php?ID=" + _playerInfo.ID);

        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            var result = int.TryParse(req.downloadHandler.text, out var number);
            if(result)
            {
                _playerInfo.Coin = number;
            }
            Debug.Log("User Coin successfully receipt -> " + req.downloadHandler.text);
            _coin.LoadCoin();
        }
        else
        {
            Debug.LogWarning("User Coin receipt failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateLevelCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewLevel", _playerInfo.Level);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/level-up.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User LVL UP successfully");
        }
        else
        {
            Debug.LogWarning("User LVL UP failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateExperienceCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewXP", _playerInfo.Experience);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/Update/experience.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User XP Update successfully");
        }
        else
        {
            Debug.LogWarning("User XP Update failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateHealthAndManaCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewHealth", _playerInfo.Health);
        form.AddField("NewMana", _playerInfo.Mana);
        
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/health-mana.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User HealthMana Update successfully");
        }
        else
        {
            Debug.LogWarning("User HealthMana Update failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateMaxHealthAndMaxManaCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewHealth", _playerInfo.MaxHealth);
        form.AddField("NewMana", _playerInfo.MaxMana);

        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/max-health-mana.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User MaxHealthMaxMana Update successfully");
        }
        else
        {
            Debug.LogWarning("User MaxHealthMaxMana Update failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateProfilePictureCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewProfilePicture", _playerInfo.ProfilePicture);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/profile-picture.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User ProfilePicture Update successfully");
        }
        else
        {
            Debug.LogWarning("User ProfilePicture Update failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateSkillPointCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewSkillPoint", _playerInfo.SkillPoint);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/skill-point.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User SkillPoint Update successfully");
        }
        else
        {
            Debug.LogWarning("User SkillPoint Update failed: # " + req.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateCoinCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        form.AddField("NewCoin", _playerInfo.Coin);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/coin.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User Coin Update successfully");
        }
        else
        {
            Debug.LogWarning("User Coin Update failed: # " + req.downloadHandler.text);
        }
    }
}
