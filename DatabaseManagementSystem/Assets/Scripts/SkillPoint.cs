using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillPoint : MonoBehaviour
{
    public TMP_Text SkillText;
    private PlayerInfo _playerInfo;
    private DBManager _dbManager;

    private void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _dbManager = GetComponent<DBManager>();
    }

    public void LoadSkillPoint()
    {
        SkillText.text = _playerInfo.SkillPoint.ToString();
    }

    public void IncreaseSkillPoint()
    {
        _playerInfo.SkillPoint++;
        LoadSkillPoint();
        StartCoroutine(_dbManager.UpdateSkillPointCo());
    }
    
    public void DecreaseSkillPoint()
    {
        _playerInfo.SkillPoint--;
        LoadSkillPoint();
        StartCoroutine(_dbManager.UpdateSkillPointCo());
    }
    
}
