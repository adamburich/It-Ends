using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClickToPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            LoadGame();
        }
    }
}
