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

    void Start()
    {
        ref GameManager gManager = ref GameManager.manager;
        gManager.uiManager = this;

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
}
