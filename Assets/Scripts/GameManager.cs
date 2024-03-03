using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] GameObject[] platforms;
    Vector3[] platformPositions;
	public static GameManager manager;
    public UIManager uiManager;
    public ResetPlatforms rPlatforms;
    public float maxLevelTimer = 0f;
    public float levelTimer = 0f; //current Time
    public bool countingTime = false;

    void Awake()
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
    }

    void Start()
    {
        levelTimer = maxLevelTimer;
    }

    void Update()
    {
        if(countingTime)
        {
            if(levelTimer <= 0) 
            {
                GameManager.manager.uiManager.SceneChange("Defeat");
                countingTime = false;
            }
            levelTimer -= Time.deltaTime;
        }
    }

    public void TimerStart()
    {
        countingTime = true;
        levelTimer = maxLevelTimer;
    }
}
