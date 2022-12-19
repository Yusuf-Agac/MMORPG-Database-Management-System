using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeSkillBar : MonoBehaviour
{
    private SelectedSkill _selectedSkill;
    private PlayerInfo _playerInfo;
    private SkillBarPieceInfo _skillBarPieceInfo;

    private void Start()
    {
        _playerInfo = GameObject.Find("Canvas").GetComponent<PlayerInfo>();
        _selectedSkill = GameObject.Find("Canvas").GetComponent<SelectedSkill>();
        _skillBarPieceInfo = transform.parent.transform.Find(gameObject.name[0] + "GridPiece").GetComponent<SkillBarPieceInfo>();
    }

    public void ChangeSelectedSkillBarSprite()
    {
        Image temp = transform.parent.transform.Find(gameObject.name[0] + "GridPiece").GetChild(0).GetChild(0).GetComponent<Image>();
        temp.sprite = _selectedSkill.SelectedSkillSprite;
        temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 1);

        StartCoroutine(ChangeSelectedSkillBarCo(temp.sprite.name, _skillBarPieceInfo.Index));
    }
    
    IEnumerator ChangeSelectedSkillBarCo(string skillName, int skillIndex)
    {
        WWWForm form = new WWWForm();
        form.AddField("SkillName", skillName);
        form.AddField("SkillBarIndex", skillIndex);
        form.AddField("ID", _playerInfo.ID.ToString());
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UpdateSkillBar.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("UpdateSkillBar successfully");
        }
        else
        {
            Debug.LogWarning("UpdateSkillBar failed: # " + req.downloadHandler.text);
        }
    }
}
