using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    private static string _username;
    private static CanvasManager _canvasManager;

    static void Start()
    {
        //_canvasManager = ;
    }
    public static void LoadUser(string username)
    {
        GameObject.Find("Canvas").GetComponent<CanvasManager>().InGameOpen();
        LoadUsername(username);
        Debug.Log("User load successfully");
    }
    
    private static void LoadUsername(string input)
    {
        _username = input;
        GameObject.FindWithTag("UsernameUI").GetComponent<TMPro.TextMeshProUGUI>().text = _username;
        Debug.Log("Username load successfully");
    }
}
