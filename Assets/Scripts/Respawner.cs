using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Diagnostics;
using System;

public class Respawner : MonoBehaviour
{
    public GameObject deathTiles;
    public static GameObject player;
    public Transform groundCheck;
    public LayerMask flooring;
    public static GameObject spawnPoint;
    private static Stopwatch completionTimer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void restart()
    {
        completionTimer = new Stopwatch();
        completionTimer.Start();
        player.transform.position = spawnPoint.transform.position;
        //GameObject.Find("ItsEnding").active = false;
        //GameObject.Find("PauseMenu").active = false;
    }

    public static string getCompletionTime()
    {
        TimeSpan ts = completionTimer.Elapsed;
        string finishTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        return finishTime;

    }

    private void FixedUpdate()
    {
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, .2f, flooring);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != gameObject)
            {
                //Debug.Log(groundColliders[i].name);
                if (groundColliders[i].gameObject.name.Contains("Death"))
                {
                    restart();
                } 
            }
        }
    }
}
