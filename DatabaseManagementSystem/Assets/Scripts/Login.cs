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
    private Button _registerButton;
    
    private GameObject LoadingAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        _username = GameObject.Find("Username").GetComponent<TMP_InputField>();
        _password = GameObject.Find("Password").GetComponent<TMP_InputField>();
        _loginButton = GameObject.Find("Login").GetComponent<Button>();
        _registerButton = GameObject.Find("Register").GetComponent<Button>();
        LoadingAnim = GameObject.Find("LoginAnim").gameObject;
        LoadingAnim.SetActive(false);
    }

    public void LoginButton()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        _loginButton.interactable = false;
        _registerButton.interactable = false;
        _username.interactable = false;
        _password.interactable = false;
        LoadingAnim.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("username", _username.text);
        form.AddField("password", _password.text);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);
        
        yield return req.SendWebRequest();
        yield return new WaitForSeconds(3f);
        
        if (req.downloadHandler.text == "0")
        {
            DBManager.LoadUser(_username.text);
            _loginButton.interactable = false;
            _registerButton.interactable = false;
            _username.interactable = false;
            _password.interactable = false;
            Debug.Log("User successfully logged");
        }
        else
        {
            _loginButton.interactable = true;
            _registerButton.interactable = true;
            _username.interactable = true;
            _password.interactable = true;
            Debug.Log("User login failed: # " + req.downloadHandler.text);
        }
        LoadingAnim.SetActive(false);
    }


    public void VerifyRules()
    {
        _loginButton.interactable = (_username.text.Length >= 8 && _password.text.Length >= 8);
    }
}

