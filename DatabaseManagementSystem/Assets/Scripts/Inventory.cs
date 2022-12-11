using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] grid;
    private Transform _content;
    private int _childrenCount;
    
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
}
