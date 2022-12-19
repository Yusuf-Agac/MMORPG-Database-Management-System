using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarPieceInfo : MonoBehaviour
{
    public int Index;
    
    void Awake()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == transform.name)
            {
                Index = i - 7;
            }
        }
    }

}
