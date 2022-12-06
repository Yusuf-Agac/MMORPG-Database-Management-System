using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    private static string _username;
    
    public static void LoadUser(string username)
    {
        LoadUsername(username);
        Debug.Log("User load successfully");
    }
    
    private static void LoadUsername(string input)
    {
        _username = input;
        Debug.Log("Username load successfully");
    }
}
