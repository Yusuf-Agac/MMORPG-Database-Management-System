using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CanvasManager : MonoBehaviour
{
    private GameObject _inGame;
    private GameObject _loginAndRegister;
    private Inventory _inventory;
    void Start()
    {
        _inGame = transform.Find("InGame").gameObject;
        _loginAndRegister = transform.Find("LoginAndRegister").gameObject;
        _inventory = transform.Find("InGame").Find("Inventory").GetComponent<Inventory>();
    }

    public void InGameOpen()
    {
        _inGame.SetActive(true);
        _loginAndRegister.SetActive(false);
        _inventory.CreateGridArray();
        Debug.Log("Inventory created successfully");
    }
}
