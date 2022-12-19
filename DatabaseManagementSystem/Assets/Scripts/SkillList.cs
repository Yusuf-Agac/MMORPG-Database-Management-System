using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SkillList : MonoBehaviour
{
    [System.Serializable]
    public class Skill
    {
        public string SkillName;
        public bool Learned = false;
        public GameObject SelectButton;
        public GameObject LearnButton;
        public Image OpenImage;
    }
    
    public Skill[] skills;
    
    private Transform _content;
    private int _childrenCount;
    private DBManager _dbManager;
    private PlayerInfo _playerInfo;
    
    bool result;
    int number;
    
    private void Awake()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _content = GameObject.Find("Canvas").transform.Find("InGame").Find("Skills").Find("Viewport").Find("Content");
        _childrenCount = _content.childCount;
        skills = new Skill[_childrenCount];
        for (var i = 0; i < _childrenCount; i++)
        {
            skills[i] = new Skill();
            skills[i].SkillName = _content.GetChild(i).GetChild(1).GetComponent<Image>().sprite.name;
            skills[i].OpenImage = _content.GetChild(i).GetChild(1).GetComponent<Image>();
            skills[i].SelectButton = _content.GetChild(i).GetChild(2).gameObject;
            skills[i].LearnButton = _content.GetChild(i).GetChild(0).gameObject;
        }
    }
    
    public void LoadSkills()
    {
        StartCoroutine(LoadSkillsCo());
    }
    
    IEnumerator LoadSkillsCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/GetSkills.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            string[] SkillsResult = req.downloadHandler.text.Split('/');
            SkillsResult = SkillsResult.Reverse().Skip(1).Reverse().ToArray();
            foreach (string skill in SkillsResult)
            {
                AddToSkillsList(skill);
            }
            Debug.Log("Skills loaded successfully");
        }
        else
        {
            Debug.LogWarning("Skills loading failed: # " + req.downloadHandler.text);
        }
    }

    public void AddToSkillsList(string skill)
    {
        for (int i = 0; i < _childrenCount; i++)
        {
            if (skills[i].SkillName == skill)
            {
                skills[i].Learned = true;
                skills[i].OpenImage.gameObject.SetActive(true);
                skills[i].SelectButton.SetActive(true);
                skills[i].LearnButton.SetActive(false);
            }
        }
    }
}
