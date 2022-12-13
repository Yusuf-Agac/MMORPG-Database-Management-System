using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CanvasManager : MonoBehaviour
{
    private GameObject _inGame;
    private GameObject _loginAndRegister;
    private Inventory _inventory;
    private DBManager _dbManager;
    private CreateItem _createItem;
    void Start()
    {
        _inGame = transform.Find("InGame").gameObject;
        _loginAndRegister = transform.Find("LoginAndRegister").gameObject;
        _inventory = transform.Find("InGame").Find("Inventory").GetComponent<Inventory>();
        _dbManager = GetComponent<DBManager>();
        _createItem = transform.Find("InGame").Find("Inventory").GetComponent<CreateItem>();
    }

    public void InGameOpen()
    {
        _inGame.SetActive(true);
        _loginAndRegister.SetActive(false);
        Debug.Log("Inventory created successfully");
    }
}
