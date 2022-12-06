using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabBetween : MonoBehaviour
{
    private TMP_InputField _fieldUsername;
    private TMP_InputField _fieldPassword;
    void Start()
    {
        _fieldPassword = GameObject.Find("Password").gameObject.GetComponent<TMP_InputField>();
        _fieldUsername = GameObject.Find("Username").gameObject.GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _fieldPassword.isFocused)
        {
            _fieldUsername.ActivateInputField();
            _fieldPassword.DeactivateInputField();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && _fieldUsername.isFocused)
        {
            _fieldUsername.DeactivateInputField();
            _fieldPassword.ActivateInputField();
        }
    }
}
