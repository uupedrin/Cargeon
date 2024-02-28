using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float force;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        body.AddForce(force * Vector3.right);
    }
}
