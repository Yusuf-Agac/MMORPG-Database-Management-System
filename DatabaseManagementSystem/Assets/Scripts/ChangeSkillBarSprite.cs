using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkillBarSprite : MonoBehaviour
{
    private SelectedSkill _selectedSkill;

    private void Start()
    {
        _selectedSkill = GameObject.Find("Canvas").GetComponent<SelectedSkill>();
    }

    public void ChangeSelectedSkillBarSprite()
    {
        Image temp = transform.parent.transform.Find(gameObject.name[0] + "GridPiece").GetChild(0).GetChild(0).GetComponent<Image>();
        temp.sprite = _selectedSkill.SelectedSkillSprite;
        temp.color = new Color(temp.color.r, temp.color.g, temp.color.b, 1);
    }
}
