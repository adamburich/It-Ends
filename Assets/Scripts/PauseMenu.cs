using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject UI_PauseMenu;
    public GameObject UI_EndOfGame;
    public GameObject spawnPoint;
    public GameObject player;
    public Text difficultyText;
    private static bool hasUsedEasy = false;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && !UI_EndOfGame.active)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public static bool triedEasy()
    {
        return hasUsedEasy;
    }

    public void Resume()
    {
        UI_PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        hasUsedEasy = false;
        TriggerText.resetTextChecks();
        GameEnd.ending = false;
        GameEnd.resetTimer();
        player.transform.position = spawnPoint.transform.position;
        if (GameObject.Find("ItsEnding"))
        {
            GameObject.Find("ItsEnding").SetActive(false);
        }
        UI_PauseMenu.SetActive(false);
        Resume();

    }
    void Pause()
    {
        UI_PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void CycleDifficulty()
    {
        if (player.GetComponent<PlayerController>().getDifficulty() == 0)
        {
            player.GetComponent<PlayerController>().setDifficulty(1);
            difficultyText.text = "Difficulty: Default";
        }
        else
        {
            player.GetComponent<PlayerController>().setDifficulty(0);
            difficultyText.text = "Difficulty: Easier";
            if (!hasUsedEasy)
            {
                hasUsedEasy = true;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
