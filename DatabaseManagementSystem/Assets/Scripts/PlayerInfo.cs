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
    private Text _levelText;

    private void Start()
    {
        _expProgressBar = GameObject.Find("Canvas").GetComponent<ExpProgressBar>();
        _levelText = GameObject.Find("Canvas").transform.Find("InGame").Find("CharacterProfile").Find("Level").Find("Text").GetComponent<Text>();
    }

    public void LoadLevelToUI()
    {
        _levelText.text = Level.ToString();
    }
    
    public void GetXp()
    {
        Experience += 30;
        if (Experience >= (Level * 100))
        {
            Experience = Experience % (Level * 100);
            LevelUp();
        }
        _expProgressBar.UpdateProgressBar();
        StartCoroutine(LoadXpCo());
    }

    private void LevelUp()
    {
        Level++;
        _expProgressBar.UpdateProgressBar();
        LoadLevelToUI();
        StartCoroutine(LevelUpCo());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator LevelUpCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("NewLevel", Level);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UserLvlUp.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User LVL UP successfully");
        }
        else
        {
            Debug.Log("User LVL failed: # " + req.downloadHandler.text);
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoadXpCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("NewXP", Experience);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UserXpUpdate.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("User XP Update successfully");
        }
        else
        {
            Debug.Log("User XP Update failed: # " + req.downloadHandler.text);
        }
    }
}