using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text timer;
	[SerializeField] private TMP_Text timerGame;
	[SerializeField] private bool shouldStartPaused;
	[SerializeField] private Slider playerHealth;

	void Start()
	{
		ref GameManager gManager = ref GameManager.manager;
		gManager.uiManager = this;

		if(shouldStartPaused)
		{
			gManager.SetPause(0);
		}
		else
		{
			gManager.SetPause(1);
		}
		
		if(timer != null)
		{
			timer.text = (GameManager.manager.maxLevelTimer - GameManager.manager.levelTimer).ToString();
		}
	}

	void Update()
	{
		if(timerGame != null)
		{
			timerGame.text = GameManager.manager.levelTimer.ToString();
		}
	}

	public void SceneChange(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	
	public void TogglePause(int set = -1)
	{
		GameManager.manager.SetPause(set);
	}
	
	public void UpdatePlayerHealth(int amount)
	{
		playerHealth.value = amount;
	}
}
