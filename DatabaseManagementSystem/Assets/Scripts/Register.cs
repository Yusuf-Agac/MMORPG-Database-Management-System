using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    private TMP_InputField _username;
    private TMP_InputField _password;
    private Button _registerButton;
    private Button _loginButton;

    private GameObject LoadingAnim;

    void Start()
    {
        _username = GameObject.Find("Username").GetComponent<TMP_InputField>();
        _password = GameObject.Find("Password").GetComponent<TMP_InputField>();
        _registerButton = GameObject.Find("Register").GetComponent<Button>();
        _loginButton = GameObject.Find("Login").GetComponent<Button>();
        LoadingAnim = GameObject.Find("RegisterAnim").gameObject;
        LoadingAnim.SetActive(false);
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterCo());
    }

    IEnumerator RegisterCo()
    {
        _registerButton.interactable = false;
        _loginButton.interactable = false;
        _username.interactable = false;
        _password.interactable = false;
        LoadingAnim.SetActive(true);
        
        WWWForm form = new WWWForm();
        form.AddField("username", _username.text);
        form.AddField("password", _password.text);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        yield return new WaitForSeconds(3f);
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User successfully created");
        }
        else
        {
            Debug.LogError("User creation failed: # " + req.downloadHandler.text);
        }
        _registerButton.interactable = true;
        _loginButton.interactable = true;
        _username.interactable = true;
        _password.interactable = true;
        LoadingAnim.SetActive(false);
    }

    public void VerifyRules()
    {
        //_registerButton.interactable = (_username.text.Length >= 8 && _password.text.Length >= 8 && IsNumber(_username.text[0]));
    }

    private bool IsNumber(char c)
    {
        char[] COLLECTION = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        foreach (var VARIABLE in COLLECTION)
        {
            if (c == VARIABLE)
            {
                return false;
            }
        }

        return true;
    }
}
