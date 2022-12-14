using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExpProgressBar : MonoBehaviour
{
    private RectTransform _expProgressBarTransform;
    private PlayerInfo _playerInfo;
    private Vector3 _localScale;
    private readonly Vector3 _pivotForDividedByZero = new Vector3(0.001f, 0, 0);

    private void Start()
    {
        _localScale = Vector3.one;
        _expProgressBarTransform = GameObject.Find("Canvas").transform.Find("InGame").Find("XPBar").Find("XPBarMask").GetComponent<RectTransform>();
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        _expProgressBarTransform.localScale = Vector3.Lerp(_expProgressBarTransform.localScale, _localScale + _pivotForDividedByZero, Time.deltaTime * 10);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateProgressBar()
    {
        _localScale = new Vector3((float)_playerInfo.Experience / (float)(_playerInfo.Level * 100), _localScale.y, _localScale.z);
        
        Debug.Log("XP/Local Scale X: " + _localScale.x + "/Experience: " + _playerInfo.Experience + "/Level: " + _playerInfo.Level.ToString());
    }
}