using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] platforms;
    Vector3[] platformPositions;
	public static GameManager manager;
    public UIManager uiManager;
    public float maxLevelTimer = 0f;
    public float levelTimer = 0f; //current Time

    void Start()
    {
        if (manager == null)
		{
			manager = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(gameObject);

        if(platforms!=null && platforms.Length>0)
        {
            platformPositions = new Vector3[platforms.Length];
            for(int i=0; i<platforms.Length; i++)
            {
                platformPositions[i] = platforms[i].transform.position;
                Debug.Log(platformPositions[i]);
            }
        } 
    }

    public void RearrangePlatforms()
    {
        for(int i=0; i<platforms.Length; i++)
        {
            platforms[i].transform.position = platformPositions[i];
            platforms[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            platforms[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }
}
