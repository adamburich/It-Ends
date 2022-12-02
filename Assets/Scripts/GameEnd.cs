using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Diagnostics;
using System;


public class GameEnd : MonoBehaviour
{
    public GameObject gameEnd;
    public static bool ending = false;
    private static Stopwatch completionTimer;
    // Start is called before the first frame update
    void Start()
    {
        completionTimer = new Stopwatch();
        completionTimer.Start();
    }

    public static string getCompletionTime()
    {
        TimeSpan ts = completionTimer.Elapsed;
        string finishTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        return finishTime;

    }
    public static bool isEnding()
    {
        return ending;
    }

    public static void resetTimer()
    {
        completionTimer = new Stopwatch();
        completionTimer.Start();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("REACHED END");
        if (other.CompareTag("Player"))
        {
            //call to stop completion timer 
            ending = true;
            gameEnd.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(completionTimer.Elapsed);
    }
}
