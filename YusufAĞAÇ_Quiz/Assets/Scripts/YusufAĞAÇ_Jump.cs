using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YusufAĞAÇ_Jump : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
