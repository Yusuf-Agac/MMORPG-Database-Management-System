using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryDragAndDrop : MonoBehaviour
{
    private bool _isLocked;
    private GameObject _lockedObject;
    private GameObject _lockedObjectsParent;
    private Vector3 _mousePos;
    public LayerMask layerMaskInventoryGrid;
    private Camera _cam;
    private Canvas _canvas;
    private Inventory _inventory;
    
    void Start()
    {
        _isLocked = false;
        _cam = Camera.main;
        _canvas = GetComponent<Canvas>();
        _inventory = GetComponent<Inventory>();
    }


    private void Update()
    {
        _mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            RayTraceForLocking();
        }
        else if (_lockedObject && _isLocked && !Input.GetMouseButton(0))
        {
            RayTraceForSwapping();
        }

        if (_isLocked && _lockedObject)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out pos);
            _lockedObject.transform.position = _canvas.transform.TransformPoint(pos);
        }
    }
    
    private void RayTraceForLocking()
    {
        Ray ray = _cam.ScreenPointToRay(_mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100, layerMaskInventoryGrid))
        {
            if (hit.transform.childCount > 0 && hit.transform.GetChild(0))
            {
                _isLocked = true;
                _lockedObject = hit.transform.GetChild(0).gameObject;
                _lockedObjectsParent = _lockedObject.transform.parent.gameObject;
                _lockedObject.transform.SetParent(_canvas.transform);
            }
        }
    }
    
    private void RayTraceForSwapping()
    {
        Ray ray = _cam.ScreenPointToRay(_mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100, layerMaskInventoryGrid))
        {
            if (_lockedObject)
            {
                if (hit.transform.childCount > 0)
                {
                    hit.transform.GetChild(0).SetParent(_lockedObjectsParent.transform);
                }
                _lockedObject.transform.SetParent(hit.transform);
                hit.transform.GetChild(0).transform.localPosition = Vector3.zero;
                if (_lockedObjectsParent.transform.childCount > 0)
                {
                    _lockedObjectsParent.transform.GetChild(0).localPosition = Vector3.zero;
                }

                if (hit.transform.childCount > 0 && _lockedObjectsParent.transform.childCount > 0)
                {
                    ItemInfo item1 = hit.transform.GetChild(0).GetComponent<ItemInfo>(), item2 = _lockedObjectsParent.transform.GetChild(0).GetComponent<ItemInfo>();
                    int tmpIndex = item1.ItemIndex;
                    Debug.Log(item1.ItemID.ToString() + " " + item1.ItemIndex + "----" + item2.ItemID.ToString() + " " + item2.ItemIndex);
                    SwapItemFunc(item1.ItemID, item2.ItemIndex);
                    SwapItemFunc(item2.ItemID, tmpIndex);
                }
                else if (hit.transform.childCount > 0)
                {
                    ItemInfo item1 = hit.transform.GetChild(0).GetComponent<ItemInfo>();
                    SwapItemFunc(item1.ItemID, hit.transform.GetComponent<GridPieceInfo>().Index);
                }
            }

            _lockedObjectsParent = null;
            _isLocked = false;
        }
        else
        {
            _isLocked = false;
            _lockedObject.transform.SetParent(_lockedObjectsParent.transform);
            _lockedObject.transform.localPosition = Vector3.zero;
            _lockedObjectsParent = null;
        }
    }
    
    public void SwapItemFunc(int itemID, int itemIndex)
    {
        StartCoroutine(SwapItemCo(itemID, itemIndex));
    }

    IEnumerator SwapItemCo(int itemID, int itemIndex)
    {
        WWWForm form = new WWWForm();
        form.AddField("ItemID", itemID);
        form.AddField("ItemIndex", itemIndex);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/UPDATE/item-index.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text == "0")
        {
            Debug.Log("Item successfully swapped");
        }
        else
        {
            Debug.LogWarning("Item swapping failed: # " + req.downloadHandler.text);
        }
    }
}
