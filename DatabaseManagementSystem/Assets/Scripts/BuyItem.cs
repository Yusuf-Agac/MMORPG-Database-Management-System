using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private Inventory _inventory;
    private CreateItem _createItem;
    private PlayerInfo _playerInfo;
    private Coin _coin;

    private void Start()
    {
        _inventory = GameObject.Find("Canvas").GetComponent<Inventory>();
        _createItem = GameObject.Find("Canvas").GetComponent<CreateItem>();
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
        _coin = GameObject.Find("Canvas").GetComponent<Coin>();
    }

    public void Buy()
    {
        for (int i = 0; i < _inventory.grid.Length; i++)
        {
            Debug.Log(_inventory.grid[i].transform.childCount + " " + _inventory.grid[i].name);
            if (_inventory.grid[i].transform.childCount == 0)
            {
                if (_playerInfo.Coin >= int.Parse(transform.parent.GetChild(3).GetComponent<TMP_Text>().text.Split(' ').Last()))
                {
                    _coin.RemoveCoin(int.Parse(transform.parent.GetChild(3).GetComponent<TMP_Text>().text.Split(' ').Last()));
                    _createItem.CreateItemFuncWithParameter(i, transform.parent.GetChild(1).GetComponent<Image>().sprite.name);
                }
                break;
            }
        }
    }
}