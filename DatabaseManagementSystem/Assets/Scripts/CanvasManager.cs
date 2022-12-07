using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CanvasManager : MonoBehaviour
{
    private GameObject _inGame;
    private GameObject _loginAndRegister;
    void Start()
    {
        _inGame = transform.Find("InGame").gameObject;
        _loginAndRegister = transform.Find("LoginAndRegister").gameObject;
    }

    public void InGameOpen()
    {
        _inGame.SetActive(true);
        _loginAndRegister.SetActive(false);
    }
}
