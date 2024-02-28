using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] float rotationScale;
    [SerializeField] GameObject player;

    void FixedUpdate()
    {
        transform.Rotate(0, Lateral() * rotationScale, 0, Space.World);
        if(Input.GetKey(KeyCode.Space)) 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }

    int Lateral()
    {
        int x = 0;
        if(Input.GetKey(KeyCode.Q)) x = -1;
        else if(Input.GetKey(KeyCode.E)) x = 1;
        return x; 
    }
}
