using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CreateItem : MonoBehaviour
{
    private DBManager _dbManager;
    public TMP_Dropdown ItemTypeDD;
    public TMP_Dropdown ItemIndexDD;
    private Inventory _inventory;

    public GameObject ItemPrefab;
    
    private void Start()
    {
        _dbManager = GameObject.Find("Canvas").GetComponent<DBManager>();
        _inventory = GetComponent<Inventory>();
    }

    public void CreateItemFunc()
    {
        StartCoroutine(CreateItemCo(int.Parse(ItemIndexDD.options[ItemIndexDD.value].text), ItemTypeDD.options[ItemTypeDD.value].text));
    }

    IEnumerator CreateItemCo(int itemIndex, string itemName)
    {
        Debug.Log("ID --> " + _dbManager.ID);
        WWWForm form = new WWWForm();
        form.AddField("ItemName", itemName);
        form.AddField("ItemIndex", itemIndex);
        form.AddField("ID", _dbManager.ID.ToString());
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/createItem.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            LoadItemToUI(itemIndex, itemName);
            Debug.Log("Item successfully created");
        }
        else
        {
            Debug.Log("Item creation failed: # " + req.downloadHandler.text);
        }
    }

    public void LoadItemToUI(int itemIndex, string itemName)
    {
        Debug.Log(_inventory.grid[itemIndex].name);
        Instantiate(ItemPrefab, _inventory.grid[itemIndex-1].transform);
    }
}
