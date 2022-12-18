using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSelectedSkill : MonoBehaviour
{
    private SelectedSkill _selectedSkill;
    
    void Start()
    {
        _selectedSkill = GameObject.Find("Canvas").GetComponent<SelectedSkill>();
    }

    public void ChangeSelected()
    {
        _selectedSkill.SelectedSkillSprite = transform.parent.GetChild(1).GetComponent<Image>().sprite;
    }
}
