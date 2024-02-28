using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float moveForce;
    [SerializeField] float brakeForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotateSpeed;


    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        body.AddForce(v * moveForce * transform.forward);
        transform.Rotate(h * rotateSpeed * transform.up);
        if(Input.GetKey(KeyCode.Space)) body.AddForce(body.velocity * -1 * brakeForce);
        if(body.velocity.magnitude > maxSpeed) body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
        Debug.Log(body.velocity.magnitude);
    }
}
