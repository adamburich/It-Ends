using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    public Text target;
    public float delay = .075f;
    private string whatdoidotext = "Must be an opening at the top if the rain can get in.";
    private string whereamitext = "What.. where am I..?";
    private static bool firstLanding = true;
    private static long falls = 0;
    private string icandoit = "I can do it. I will escape this pit.";
    private string almostthere = "Not much farther now.";
    private string madeit = "You did it!!!";
    private string justwantout = "Get me out of here.";
    private string jumpthroughtext = "I bet I can jump through these little platforms.";
    private static bool gavejumpthruhint = false;
    private string cantbemuchbigger = "This place can't be much bigger can it?";
    private static bool beenthere = false;
    private static bool askedyet = false;
    private static bool wantingout = false;
    private static bool madeitbefore = false;
    public AudioSource textBlip;
    private static bool introd = false;
    public Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void resetTextChecks()
    {
        firstLanding = true;
        introd = false;
        falls = 0; 
        beenthere = false;
        madeitbefore = false;
        wantingout = false;
        askedyet = false;
        gavejumpthruhint = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == ("IntroFloor") && !introd)
        {

            StartCoroutine(genText(whatdoidotext));
            introd = true;
        }
        else if(collision.collider.tag == ("FirstLanding"))
        {
            if (firstLanding)
            {
                StartCoroutine(genText(whereamitext));
                firstLanding = false;
            }
            else if(falls % 10 == 0 && introd)
            {
                StartCoroutine(genText(icandoit));
            }
            falls += 1;
        }
        else if(collision.collider.tag == ("MadeIt") && !madeitbefore)
        {
            StartCoroutine(genText(madeit));
            madeitbefore = true;

        }
        else if (collision.collider.tag == ("JumpThroughTrigger") && !gavejumpthruhint)
        {
            StartCoroutine(genText(jumpthroughtext));
            gavejumpthruhint = true;
        }
        else if(collision.collider.tag == "AlmostThere" && !beenthere)
        {
            StartCoroutine(genText(almostthere));
            beenthere = true;
        }
        else if(collision.collider.tag == "JustWantOut" && !wantingout)
        {
            StartCoroutine(genText(justwantout));
            wantingout = true;
        }
        else if(collision.collider.tag == "CantBeMuchBigger" && !askedyet)
        {
            StartCoroutine(genText(cantbemuchbigger));
            askedyet = true;
        }
    }

    IEnumerator genText(string textToGen)
    {
        string currentText = "";
        if (textToGen == (whereamitext))
        {
            yield return new WaitForSeconds(3);
        }
        foreach (char c in textToGen)
        {
            currentText += c;
            target.text = currentText;
            textBlip.Play();
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(5);
        target.text = "";
        textToGen = "";
    }
}
