using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int ItemID;
        public string ItemName;
        public int ItemIndex;
        public int ID;
    }
    
    public Item[] items;

    public string Username;
    public string ID;
    private GameObject _canvas;
    private CreateItem _createItem;
    public CanvasManager _canvasManager;

    bool result;
    int number;
    
    void Start()
    {
        items = new Item[30];
        _canvas = GameObject.Find("Canvas");
        _createItem = _canvas.transform.Find("InGame").Find("Inventory").GetComponent<CreateItem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadInventory();
        }
    }

    public void LoadUser(string username)
    {
        GameObject.Find("Canvas").GetComponent<CanvasManager>().InGameOpen();
        LoadUsername(username);
        Debug.Log("User load successfully");
        LoadID();
        Debug.Log("ID load successfully");
    }
    
    private void LoadUsername(string input)
    {
        Username = input;
        GameObject.FindWithTag("UsernameUI").GetComponent<TMPro.TextMeshProUGUI>().text = Username;
        Debug.Log("Username load successfully");
    }
    
    private void LoadID()
    {
        StartCoroutine(LoadIDCo());
    }

    IEnumerator LoadIDCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", Username);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getUserID.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "-1")
        {
            ID = req.downloadHandler.text;
            Debug.Log("User ID successfully logged ->>>>> " + ID.ToString());
        }
        else
        {
            Debug.Log("User login failed: # " + req.downloadHandler.text);
        }
    }

    private void LoadInventory()
    {
        StartCoroutine(LoadInventoryCo());
    }
    
    IEnumerator LoadInventoryCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getInventory.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            string[] InventoryResult = req.downloadHandler.text.Split('/');
            InventoryResult = InventoryResult.Reverse().Skip(1).Reverse().ToArray();
            
            int counter = 0;
            foreach (string ItemResult in InventoryResult)
            {
                string[] ItemInfoResult = ItemResult.Split(',');
                
                result = int.TryParse(ItemInfoResult[0], out number);
                if(result)
                {
                    items[counter].ItemID = number;
                }
                items[counter].ItemName = ItemInfoResult[1];
                result = int.TryParse(ItemInfoResult[2], out number);
                if(result)
                {
                    items[counter].ItemIndex = number;
                }
                result = int.TryParse(ItemInfoResult[3], out number);
                if(result)
                {
                    items[counter].ID = number;
                }
                counter++;
                if(counter==26){break;}
            }
        }
        else
        {
            Debug.Log("Inventory loading failed: # " + req.downloadHandler.text);
        }
    }
}
