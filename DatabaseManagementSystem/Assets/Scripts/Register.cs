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
    private Button _submitButton;

    void Start()
    {
        _username = GameObject.Find("Username").GetComponent<TMP_InputField>();
        _password = GameObject.Find("Password").GetComponent<TMP_InputField>();
        _submitButton = GameObject.Find("Register").GetComponent<Button>();
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterCo());
    }

    IEnumerator RegisterCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", _username.text);
        form.AddField("password", _password.text);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User successfully created");
        }
        else
        {
            Debug.Log("User creation failed: # " + req.downloadHandler.text);
        }
    }

    public void VerifyRules()
    {
        _submitButton.interactable = (_username.text.Length >= 8 && _password.text.Length >= 8 && IsNumber(_username.text[0]));
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
