using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YusufAĞAÇ_PointSystem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public int point;

    private void Awake()
    {
        point = 0;
    }

    public void GetPoint()
    {
        point++;
        text.text = point.ToString();
        if (point >= 5)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
