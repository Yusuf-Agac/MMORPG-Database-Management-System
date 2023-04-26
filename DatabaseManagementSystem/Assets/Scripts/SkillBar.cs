using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [System.Serializable]
    public class SkillBarPiece
    {
        public string SkillName;
        public int SkillIndex;
        public bool IsEmpty;
    }
    
    public SkillBarPiece[] skillBar;
    public GameObject[] grid;

    private int _skillCounter;
    bool result;
    int number;
    private PlayerInfo _playerInfo;

    private void Awake()
    {
        _skillCounter = 0;
        _playerInfo = GetComponent<PlayerInfo>();
        skillBar = new SkillBarPiece[7];
        for (int i = 0; i < 7; i++)
        {
            skillBar[i] = new SkillBarPiece();
        }

        Transform gridTransform = GameObject.Find("Canvas").transform.Find("InGame").Find("SkillBar");
        grid = new GameObject[gridTransform.childCount];
        for (int i = 0; i < gridTransform.childCount; i++)
        {
            grid[i] = gridTransform.GetChild(i).gameObject;
        }
    }
    
    public void LoadSkillBarToUI()
    {
        foreach (var skill in skillBar)
        {
            if (skill != null && skill.SkillName!= null && !skill.IsEmpty)
            {
                LoadSkillBarPieceToUI(skill.SkillName, skill.SkillIndex);
            }
        }
    }
    
    public void LoadSkillBarPieceToUI(string skillName, int skillIndex)
    {
        Debug.Log(skillName);
        Image tmp = grid[skillIndex + 7].transform.GetChild(0).GetChild(0).GetComponent<Image>();
        tmp.sprite = Resources.Load<Sprite>("Skills/" + skillName);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1);
    }
    
    public void LoadSkillBar()
    {
        StartCoroutine(LoadSkillBarCo());
    }
    
    IEnumerator LoadSkillBarCo()
    {
        UnityWebRequest req = UnityWebRequest.Get("http://localhost/sqlconnect/GET/skill-bar.php?ID=" + _playerInfo.ID);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            string[] SkillBarResult = req.downloadHandler.text.Split('/');
            SkillBarResult = SkillBarResult.Reverse().Skip(1).Reverse().ToArray();
            string[] SkillInfoResult = new string[2];
            foreach (string SkillResult in SkillBarResult)
            {
                SkillInfoResult = SkillResult.Split(',');
                AddToSkillBarList(SkillInfoResult);
                _skillCounter++;
            }
            LoadSkillBarToUI();
            Debug.Log("SkillBar loaded successfully");
        }
        else
        {
            Debug.LogWarning("SkillBar loading failed: # " + req.downloadHandler.text);
        }
    }

    public void AddToSkillBarList(string[] SkillBarInfoResult)
    { 
        skillBar[_skillCounter].SkillName = SkillBarInfoResult[0];
        result = int.TryParse(SkillBarInfoResult[1], out number);
        if(result)
        {
            skillBar[_skillCounter].SkillIndex = number;
        }
        skillBar[_skillCounter].IsEmpty = false;
    }
}
