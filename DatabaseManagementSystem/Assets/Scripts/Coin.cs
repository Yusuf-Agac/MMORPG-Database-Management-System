using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TMP_Text CoinText;
    private PlayerInfo _playerInfo;
    private DBManager _dbManager;

    private void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _dbManager = GetComponent<DBManager>();
    }

    public void LoadCoin()
    {
        CoinText.text = _playerInfo.Coin.ToString();
    }

    public void AddCoin()
    {
        _playerInfo.Coin += Random.Range(1, 1000);
        LoadCoin();
        StartCoroutine(_dbManager.UpdateCoinCo());
    }
    
    public void RemoveCoin(int amount)
    {
        _playerInfo.Coin -= amount;
        LoadCoin();
        StartCoroutine(_dbManager.UpdateCoinCo());
    }
}
