using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YusufAĞAÇ_Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 3;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.forward * (speed * Time.deltaTime), ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.back * (speed * Time.deltaTime), ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * (speed * Time.deltaTime), ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * (speed * Time.deltaTime), ForceMode.Acceleration);
        }
    }
}
