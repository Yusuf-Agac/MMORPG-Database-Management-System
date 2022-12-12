using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int ItemID;
        public string ItemName;
        public int ItemIndex;
        public int ID;
        public bool IsEmpty = true;
    }
    
    public Item[] items;
    
    public GameObject[] grid;
    private Transform _content;
    private int _childrenCount;

    private DBManager _dbManager;
    private CreateItem _createItem;

    private Dictionary<string, GameObject> ItemPrefab;

    
    bool result;
    int number;

    private void Start()
    {
        ItemPrefab = new Dictionary<string, GameObject>()
        {
            {"Axe", Resources.Load<GameObject>("Prefabs/Axe")},
            {"Armor", Resources.Load<GameObject>("Prefabs/Armor")},
            {"Belt", Resources.Load<GameObject>("Prefabs/Belt")},
            {"Book", Resources.Load<GameObject>("Prefabs/Book")},
            {"Boot", Resources.Load<GameObject>("Prefabs/Boot")},
            {"Coin", Resources.Load<GameObject>("Prefabs/Coin")},
            {"Gem", Resources.Load<GameObject>("Prefabs/Gem")},
            {"Shield", Resources.Load<GameObject>("Prefabs/Shield")},
            {"Shoulder", Resources.Load<GameObject>("Prefabs/Shoulder")},
            {"Sword", Resources.Load<GameObject>("Prefabs/Sword")}
        };
        items = new Item[30];
        _dbManager = GameObject.Find("Canvas").GetComponent<DBManager>();
        _createItem = GetComponent<CreateItem>();
    }

    // Creating grid array with objects named InventoryPiece
    public void CreateGridArray()
    {
        _content = transform.Find("Viewport").Find("Content");
        int tempChildrenCount = _content.childCount;
        _childrenCount = 0;
        for (int i = 0; i < tempChildrenCount; ++i)
        {
            if (_content.GetChild(i).gameObject.name.Length > 13 && _content.GetChild(i).gameObject.name.Substring(0, 14) == "InventoryPiece")
            {
                _childrenCount++;
            }
        }

        grid = new GameObject[_childrenCount];
        int indexCounter = 0;
        for (int i = 0; i < tempChildrenCount; ++i)
        {
            if (_content.GetChild(i).gameObject.name.Length > 13 && _content.GetChild(i).gameObject.name.Substring(0, 14) == "InventoryPiece")
            {
                grid[indexCounter] = _content.GetChild(i).gameObject;
                indexCounter++;
            }
        }
    }
    
    void PrintInventory()
    {
        for (int i = 0; i < _childrenCount; i++)
        {
            Debug.Log(i.ToString() + ". grid --> " + grid[i].name);
        }
    }
    
    public void LoadInventoryToUI()
    {
        foreach (var item in items)
        {
            if (item != null && !item.IsEmpty)
            {
                LoadItemToUI(item.ItemIndex, item.ItemName, item.ID, item.ItemID);
            }
        }
    }
    
    public void LoadItemToUI(int itemIndex, string itemName, int ID, int itemID)
    {
        Debug.Log("Item created on --> " + itemIndex);
        Debug.Log(grid[itemIndex-1].name);
        GameObject tmp = Instantiate(ItemPrefab[itemName], grid[itemIndex-1].transform);
        ItemInfo tmpItemInfo = tmp.GetComponent<ItemInfo>();   
        tmpItemInfo.ID = ID;
        tmpItemInfo.ItemIndex = itemIndex;
        tmpItemInfo.ItemName = itemName;
        tmpItemInfo.ItemID = itemID;
    }
    
    public void LoadInventory()
    {
        StartCoroutine(LoadInventoryCo());
    }
    
    IEnumerator LoadInventoryCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _dbManager.ID);
        
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
                items[counter].IsEmpty = false;
                counter++;
                if(counter==26){break;}
            }
            LoadInventoryToUI();
        }
        else
        {
            Debug.Log("Inventory loading failed: # " + req.downloadHandler.text);
        }
    }
}
