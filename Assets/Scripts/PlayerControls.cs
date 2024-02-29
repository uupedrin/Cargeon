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
    [SerializeField] float velocityDamage;
    [SerializeField] float turboForce;
    Vector3 respawnLocation;

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
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor") respawnLocation = collision.transform.position;
        Debug.Log(respawnLocation);
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Respawn":
            body.constraints = RigidbodyConstraints.FreezeAll;
            body.constraints = RigidbodyConstraints.FreezeRotation;
            transform.Rotate(transform.rotation.eulerAngles);
            transform.position = new Vector3(respawnLocation.x, respawnLocation.y + 0.5f, respawnLocation.z);
            GameManager.manager.RearrangePlatforms();
            break;

            case "Turbo":
            body.constraints = RigidbodyConstraints.FreezeAll;
            body.constraints = RigidbodyConstraints.FreezeRotation;
            body.AddForce(-other.transform.forward * turboForce);
            break;
        }
    }
}
