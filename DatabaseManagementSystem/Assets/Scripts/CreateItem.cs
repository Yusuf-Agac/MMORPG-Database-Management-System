using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEditor.Timeline.Actions;

public class CreateItem : MonoBehaviour
{
    private DBManager _dbManager;
    public TMP_Dropdown ItemTypeDD;
    public TMP_Dropdown ItemIndexDD;
    private Inventory _inventory;

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
        itemIndex -= 1;
        Debug.Log("ID --> " + _dbManager.ID);
        WWWForm form = new WWWForm();
        form.AddField("ItemName", itemName);
        form.AddField("ItemIndex", itemIndex);
        form.AddField("ID", _dbManager.ID.ToString());
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/createItem.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        WWWForm form2 = new WWWForm();
        form2.AddField("ID", _dbManager.ID);
        form2.AddField("ItemIndex", itemIndex);
        
        UnityWebRequest req2 = UnityWebRequest.Post("http://localhost/sqlconnect/getItemID.php", form);
        req2.downloadHandler = new DownloadHandlerBuffer();
        
        yield return req2.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            _inventory.LoadItemToUI(itemIndex, itemName, int.Parse(_dbManager.ID), int.Parse(req2.downloadHandler.text));
            string[] itemInfo = new string[5];
            itemInfo[0] = req2.downloadHandler.text;
            itemInfo[1] = itemName;
            itemInfo[2] = itemIndex.ToString();
            itemInfo[3] = _dbManager.ID;
            _inventory.AddToItemsList(itemInfo);
            Debug.Log("Item successfully created");
        }
        else
        {
            Debug.Log("Item creation failed: # " + req.downloadHandler.text);
        }
    }
}
