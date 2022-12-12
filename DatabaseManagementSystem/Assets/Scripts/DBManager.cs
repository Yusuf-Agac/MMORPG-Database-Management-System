using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    

    public string Username;
    public string ID;
    private GameObject _canvas;
    private Inventory _inventory;
    private CanvasManager _canvasManager;

    void Start()
    {
        _canvas = GameObject.Find("Canvas");
        _canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        _inventory = transform.Find("InGame").Find("Inventory").GetComponent<Inventory>();
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
        Username = input;
        GameObject.FindWithTag("UsernameUI").GetComponent<TMPro.TextMeshProUGUI>().text = Username;
        Debug.Log("Username load successfully");
    }
    
    private void LoadID()
    {
        StartCoroutine(LoadIDCo());
    }

    IEnumerator LoadIDCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", Username);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getUserID.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "-1")
        {
            ID = req.downloadHandler.text;
            Debug.Log("User ID successfully logged ->>>>> " + ID.ToString());
            _inventory.LoadInventory();
        }
        else
        {
            Debug.Log("User login failed: # " + req.downloadHandler.text);
        }
    }

    
    
    
}
