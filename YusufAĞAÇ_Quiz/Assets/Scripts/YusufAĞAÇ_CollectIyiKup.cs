using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YusufAĞAÇ_CollectIyiKup : MonoBehaviour
{
    YusufAĞAÇ_PointSystem pointSystem;

    private void Awake()
    {
        pointSystem = GameObject.FindObjectOfType<YusufAĞAÇ_PointSystem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            pointSystem.GetPoint();
        }
    }
}
