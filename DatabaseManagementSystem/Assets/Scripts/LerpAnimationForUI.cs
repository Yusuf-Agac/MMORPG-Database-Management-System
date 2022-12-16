using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAnimationForUI : MonoBehaviour
{
    public Vector3 on;
    public Vector3 off;
    public float animationSpeed = 10f;
    private bool isOn = false;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectTransform.localPosition = Vector3.Lerp(_rectTransform.localPosition, isOn ? on : off, animationSpeed * Time.deltaTime);
    }

    public void ChangeState()
    {
        isOn = !isOn;
    }
}
