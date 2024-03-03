using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;

    void Start()
    {
        ref GameManager gManager = ref GameManager.manager;
        gManager.uiManager = this;

        if(timer != null)
        {
            timer.text = (gManager.maxLevelTimer - gManager.levelTimer).ToString();
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
