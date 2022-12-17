using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public string ID;
    public string Username;
    public int Level;
    public int Experience;
    public int Health;
    public int MaxHealth;
    public int Mana;
    public int MaxMana;
    public string ProfilePicture;

    private ExpProgressBar _expProgressBar;
    private HealthAndManaProgressBar _healthAndManaProgressBar;
    private Text _levelText;
    private DBManager _dbManager;
    private CanvasManager _canvasManager;

    private void Start()
    {
        _expProgressBar = GetComponent<ExpProgressBar>();
        _levelText = GameObject.Find("Canvas").transform.Find("InGame").Find("CharacterProfile").Find("Level").Find("Text").GetComponent<Text>();
        _healthAndManaProgressBar = GetComponent<HealthAndManaProgressBar>();
        _dbManager = GetComponent<DBManager>();
        _canvasManager = GetComponent<CanvasManager>();
    }

    public void LoadLevelToUI()
    {
        _levelText.text = Level.ToString();
    }
    
    public void GainExperience()
    {
        Experience += 30;
        if (Experience >= (Level * 100))
        {
            Experience = Experience % (Level * 100);
            LevelUp();
        }
        _expProgressBar.UpdateProgressBar();
        StartCoroutine(_dbManager.UpdateExperienceCo());
    }
    
    public void GetDamage()
    {
        Health -= MaxHealth / 14;
        if (Health < 0)
        {
            Health = 1;
        }
        Debug.Log("Health -> " + Health);
        _healthAndManaProgressBar.UpdateProgressBar();
        StartCoroutine(_dbManager.UpdateHealthAndManaCo());
    }
    
    public void ConsumeMana()
    {
        Mana -= MaxMana / 12;
        if (Mana < 0)
        {
            Mana = 1;
        }
        Debug.Log("Mana -> " + Mana);
        _healthAndManaProgressBar.UpdateProgressBar();
        StartCoroutine(_dbManager.UpdateHealthAndManaCo());
    }
    
    public void GetHeal()
    {
        Health += MaxHealth / 8;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        Debug.Log("Health -> " + Health);
        _healthAndManaProgressBar.UpdateProgressBar();
        StartCoroutine(_dbManager.UpdateHealthAndManaCo());
    }
    
    public void GetMana()
    {
        Mana += MaxMana / 8;
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
        Debug.Log("Mana -> " + Mana);
        _healthAndManaProgressBar.UpdateProgressBar();
        StartCoroutine(_dbManager.UpdateHealthAndManaCo());
    }

    public void LevelUp()
    {
        Level++;
        _expProgressBar.UpdateProgressBar();
        LoadLevelToUI();
        StartCoroutine(_dbManager.UpdateLevelCo());
        MaxHealth = Level * 100;
        MaxMana = Level * 100;
        StartCoroutine(_dbManager.UpdateMaxHealthAndMaxManaCo());
        _healthAndManaProgressBar.UpdateProgressBar();
        Debug.Log("LevelUp -> " + Level);
    }
    
    public void GetHealthMana()
    {
        StartCoroutine(_dbManager.GetHealthManaCo());
    }
    
    public void GetLevel()
    {
        StartCoroutine(_dbManager.GetLevelCo());
    }
    
    public void GetExperience()
    {
        StartCoroutine(_dbManager.GetExperienceCo());
    }
    
    public void GetID()
    {
        StartCoroutine(_dbManager.GetIDCo());
    }
    
    public void GetProfilePicture()
    {
        StartCoroutine(_dbManager.GetProfilePictureCo());
    }
    
    public void LoadUser()
    {
        _canvasManager.InGameOpen();
        LoadUsernameToUI();
        Debug.Log("User load successfully");
        GetID();
        Debug.Log("ID load successfully");
    }
    
    private void LoadUsernameToUI()
    {
        GameObject.FindWithTag("UsernameUI").GetComponent<TMPro.TextMeshProUGUI>().text = Username;
        Debug.Log("Username load successfully");
    }
}