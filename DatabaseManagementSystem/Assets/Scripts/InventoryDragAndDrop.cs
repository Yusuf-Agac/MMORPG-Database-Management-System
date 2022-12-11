using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    void Start()
    {
        _isLocked = false;
        _cam = Camera.main;
        _canvas = transform.parent.parent.GetComponent<Canvas>();
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
            _isLocked = true;
            _lockedObject = hit.transform.GetChild(0).gameObject;
            _lockedObjectsParent = _lockedObject.transform.parent.gameObject;
            _lockedObject.transform.SetParent(_canvas.transform);
        }
    }
    
    private void RayTraceForSwapping()
    {
        Ray ray = _cam.ScreenPointToRay(_mousePos);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100, layerMaskInventoryGrid) && hit.transform.childCount > 0)
        {
            hit.transform.GetChild(0).SetParent(_lockedObjectsParent.transform);
            _lockedObject.transform.SetParent(hit.transform);
            hit.transform.GetChild(0).transform.localPosition = Vector3.zero;
            _lockedObjectsParent.transform.GetChild(0).localPosition = Vector3.zero;
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
}
