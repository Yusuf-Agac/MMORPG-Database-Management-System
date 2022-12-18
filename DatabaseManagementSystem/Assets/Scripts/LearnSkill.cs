using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LearnSkill : MonoBehaviour
{
    private SkillList _skillList;
    private PlayerInfo _playerInfo;
    private SkillPoint _skillPoint;

    private void Start()
    {
        _skillList = GameObject.Find("Canvas").GetComponent<SkillList>();
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
        _skillPoint = GameObject.Find("Canvas").GetComponent<SkillPoint>();
    }

    public void Learn()
    {
        if (_playerInfo.SkillPoint > 0)
        {
            Image tmp = transform.parent.GetChild(1).GetComponent<Image>();
            for (int i = 0; i < _skillList.skills.Length; i++)
            {
                if (!_skillList.skills[i].Learned)
                {
                    StartCoroutine(LearnSkillCo(tmp.sprite.name, tmp));
                    break;
                }
            }
        }
    }
    
    IEnumerator LearnSkillCo(string skillName, Image tmp)
    {
        WWWForm form = new WWWForm();
        form.AddField("SkillName", skillName);
        form.AddField("ID", _playerInfo.ID);
        Debug.Log(skillName + " " + _playerInfo.ID);
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/LearnSkill.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            _skillPoint.DecreaseSkillPoint();
            _skillList.AddToSkillsList(tmp.sprite.name);
            Debug.Log("Skill successfully learned");
        }
        else
        {
            Debug.LogWarning("Skill learning failed: # " + req.downloadHandler.text);
        }
    }
}
