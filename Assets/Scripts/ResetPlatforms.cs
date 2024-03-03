using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlatforms : MonoBehaviour
{
    [SerializeField] Vector3[] platformPositions;
    void Start()
    {
        GameManager.manager.rPlatforms = this;
        GameManager.manager.TimerStart();
        platformPositions = new Vector3[transform.childCount];
        if(transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                platformPositions[i] = transform.GetChild(i).position;
            }
        }
    }

    public void RearrangePlatforms()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = platformPositions[i];
            transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }
}
