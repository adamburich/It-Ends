using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public GameObject player;
    public Text difficultyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
