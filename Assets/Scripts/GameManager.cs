using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//[SerializeField] GameObject[] platforms;
	Vector3[] platformPositions;
	public static GameManager manager;
	public UIManager uiManager;
	public EnemyManager enemyManager;
	public ResetPlatforms rPlatforms;
	public float maxLevelTimer = 0f;
	public float levelTimer = 0f; //current Time
	public bool countingTime = false;
	public bool isPaused = false;

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
	
	public void SetPause(int set = -1) //set = 1 -> unpause | set = 0-> pause | set = -1 -> toggle
	{
		switch(set)
		{
			case 0:
				Time.timeScale = 0;
				isPaused = true;
			break;
			case 1:
				Time.timeScale = 1;
				isPaused = false;
			break;
			default:
				Time.timeScale = Time.timeScale == 0 ? 1 : 0;;
				isPaused = !isPaused;
			break;
		}
	}
}
