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
    private int _itemsCounter = 0;
    public Item[] items;
    public GameObject[] grid;
    
    private Transform _content;
    private Transform _equipments;
    private int _childrenCount;
    private int _totalChildrenCount;

    private DBManager _dbManager;
    private CreateItem _createItem;
    private PlayerInfo _playerInfo;

    private Dictionary<string, GameObject> ItemPrefab;

    
    bool result;
    int number;

    private void Awake()
    {
        items = new Item[26];
        for (int i = 0; i < 26; i++)
        {
            items[i] = new Item();
        }
    }

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
        _dbManager = GetComponent<DBManager>();
        _createItem = GetComponent<CreateItem>();
        _playerInfo = GetComponent<PlayerInfo>();
    }

    // Creating grid array with objects named InventoryPiece
    public void CreateGridArray()
    {
        _equipments = GameObject.Find("Canvas").transform.Find("InGame").Find("Inventory").Find("Viewport").Find("Content").Find("Equipments");
        _content = GameObject.Find("Canvas").transform.Find("InGame").Find("Inventory").Find("Viewport").Find("Content");
        int tempChildrenCount = _content.childCount;
        tempChildrenCount += _equipments.childCount;
        _childrenCount = 0;
        _totalChildrenCount = 0;
        for (int i = 0; i < tempChildrenCount - 1; ++i)
        {
            if (i < 20 && _content.GetChild(i).gameObject.name.Length > 13 && _content.GetChild(i).gameObject.name.Substring(0, 14) == "InventoryPiece")
            {
                _childrenCount++;
                _totalChildrenCount++;
            }
            else if (i >= 20 && _equipments.childCount - 1 >= (i % 20) && _equipments.GetChild(i % 20).gameObject.name == "Equipment")
            {
                _totalChildrenCount++;
            }
        }

        grid = new GameObject[_totalChildrenCount];
        int gridIndexCounter = 0;
        for (int i = 0; i < tempChildrenCount; ++i)
        {
            if (i < 20 && _content.GetChild(i).gameObject.name.Length > 13 && _content.GetChild(i).gameObject.name.Substring(0, 14) == "InventoryPiece")
            {
                grid[gridIndexCounter] = _content.GetChild(i).gameObject;
                grid[gridIndexCounter].GetComponent<GridPieceInfo>().Index = gridIndexCounter;
                gridIndexCounter++;
            }
            else if (i >= 20 && _equipments.childCount - 1 >= (i % 20) && _equipments.GetChild(i % 20).gameObject.name == "Equipment")
            {
                grid[gridIndexCounter] = _equipments.GetChild(i % 20).gameObject;
                grid[gridIndexCounter].GetComponent<GridPieceInfo>().Index = gridIndexCounter;
                gridIndexCounter++;
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
        //Debug.Log("Item created on --> " + itemIndex);
        GameObject tmp = Instantiate(ItemPrefab[itemName], grid[itemIndex].transform);
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
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/getInventory.php", form);
        
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            string[] InventoryResult = req.downloadHandler.text.Split('/');
            InventoryResult = InventoryResult.Reverse().Skip(1).Reverse().ToArray();
            string[] ItemInfoResult = new string[5];
            foreach (string ItemResult in InventoryResult)
            {
                ItemInfoResult = ItemResult.Split(',');
                AddToItemsList(ItemInfoResult);
                _itemsCounter++;
                if(_itemsCounter==26){Debug.Log("Break"); break;}
            }
            LoadInventoryToUI();
            Debug.Log("Inventory loaded successfully");
        }
        else
        {
            Debug.LogWarning("Inventory loading failed: # " + req.downloadHandler.text);
        }
    }

    public void AddToItemsList(string[] ItemInfoResult)
    {
        result = int.TryParse(ItemInfoResult[0], out number);
        if(result)
        {
            items[_itemsCounter].ItemID = number;
        }
        items[_itemsCounter].ItemName = ItemInfoResult[1];
        result = int.TryParse(ItemInfoResult[2], out number);
        if(result)
        {
            items[_itemsCounter].ItemIndex = number;
        }
        result = int.TryParse(ItemInfoResult[3], out number);
        if(result)
        {
            items[_itemsCounter].ID = number;
        }
        items[_itemsCounter].IsEmpty = false;
    }
}
