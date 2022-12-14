using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    private GameObject _canvas;
    private Inventory _inventory;
    private CanvasManager _canvasManager;
    private PlayerInfo _playerInfo;
    private ExpProgressBar _expProgressBar;

    void Start()
    {
        _canvas = GameObject.Find("Canvas");
        _canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        _inventory = transform.Find("InGame").Find("Inventory").GetComponent<Inventory>();
        _expProgressBar = GameObject.Find("Canvas").GetComponent<ExpProgressBar>();
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
    }

    public void LoadUser(string username)
    {
        _canvasManager.InGameOpen();
        LoadUsername(username);
        Debug.Log("User load successfully");
        LoadID();
        Debug.Log("ID load successfully");
    }
    
    private void LoadUsername(string input)
    {
        _playerInfo.Username = input;
        GameObject.FindWithTag("UsernameUI").GetComponent<TMPro.TextMeshProUGUI>().text = _playerInfo.Username;
        Debug.Log("Username load successfully");
    }
    
    private void LoadID()
    {
        StartCoroutine(LoadIDCo());
    }

    IEnumerator LoadIDCo()
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
            LoadXP();
        }
        else
        {
            Debug.Log("User login failed: # " + req.downloadHandler.text);
        }
    }
    
    private void LoadXP()
    {
        StartCoroutine(LoadXPCo());
    }

    IEnumerator LoadXPCo()
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
            LoadLVL();
        }
        else
        {
            Debug.Log("User Exp failed: # " + req.downloadHandler.text);
        }
    }
    
    private void LoadLVL()
    {
        StartCoroutine(LoadLVLCo());
    }

    IEnumerator LoadLVLCo()
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
}
