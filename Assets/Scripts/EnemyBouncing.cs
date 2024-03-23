using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBouncing : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float force;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter()
    {
        Bounce();
    }

    void OnTriggerEnter()
    {
        Bounce();
    }

    void Bounce()
    {
        body.constraints = RigidbodyConstraints.FreezeAll;
        body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
        body.AddForce(Vector3.up * force);
    }
}
