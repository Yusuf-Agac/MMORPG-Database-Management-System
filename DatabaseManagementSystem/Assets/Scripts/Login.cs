using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private TMP_InputField _username;
    private TMP_InputField _password;

    private Button _loginButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _username = GameObject.Find("Username").GetComponent<TMP_InputField>();
        _password = GameObject.Find("Password").GetComponent<TMP_InputField>();
        _loginButton = GameObject.Find("Login").GetComponent<Button>();
    }

    public void LoginButton()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", _username.text);
        form.AddField("password", _password.text);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);
        
        yield return req.SendWebRequest();
        if (req.downloadHandler.text == "0")
        {
            DBManager.LoadUser(_username.text);
            Debug.Log("User successfully logged");
        }
        else
        {
            Debug.Log("User login failed: # " + req.downloadHandler.text);
        }
    }


    public void VerifyRules()
    {
        _loginButton.interactable = (_username.text.Length >= 8 && _password.text.Length >= 8);
    }
}

