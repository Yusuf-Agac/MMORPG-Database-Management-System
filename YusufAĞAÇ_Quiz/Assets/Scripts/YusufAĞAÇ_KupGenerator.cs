using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class YusufAĞAÇ_KupGenerator : MonoBehaviour
{
    public GameObject iyiKupPrefab;
    public GameObject kotuKupPrefab;

    private void Start()
    {
        StartCoroutine(GenerateKup());
    }

    IEnumerator GenerateKup()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            int random = Random.Range(0, 2);
            Instantiate(random == 0 ? iyiKupPrefab : kotuKupPrefab,
                new Vector3(Mathf.Round(Random.Range(0, 20)), 0.5f, Mathf.Round(Random.Range(0, 20))), Quaternion.identity);
        }
    }
}
