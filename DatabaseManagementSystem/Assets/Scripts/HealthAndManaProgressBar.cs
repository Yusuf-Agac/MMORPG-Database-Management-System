using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndManaProgressBar : MonoBehaviour
{
    private Image _HealthProgressBar;
    private Image _ManaProgressBar;
    private PlayerInfo _playerInfo;
    private float _localScaleHealth;
    private float _localScaleMana;

    private void Start()
    {
        _localScaleHealth = 1;
        _localScaleMana = 1;
        _HealthProgressBar = GameObject.Find("Canvas").transform.Find("InGame").Find("CharacterProfile").Find("Bars").Find("HealthBar").GetComponent<Image>();
        _ManaProgressBar = GameObject.Find("Canvas").transform.Find("InGame").Find("CharacterProfile").Find("Bars").Find("ManaBar").GetComponent<Image>();
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        _HealthProgressBar.fillAmount = Mathf.Lerp(_HealthProgressBar.fillAmount, _localScaleHealth, Time.deltaTime * 10);
        _ManaProgressBar.fillAmount = Mathf.Lerp(_ManaProgressBar.fillAmount, _localScaleMana, Time.deltaTime * 10);    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateProgressBar()
    {
        _localScaleHealth = (float)_playerInfo.Health / (float)(_playerInfo.MaxHealth);
        _localScaleMana = (float)_playerInfo.Mana / (float)(_playerInfo.MaxMana);
    }
}
