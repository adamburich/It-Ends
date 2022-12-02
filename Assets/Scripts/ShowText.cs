using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public string completionTime;
    private bool easyFinish = false;
    public float delay = .1f;
    public string entireText;
    public string currentText = "";
    public Text obj;
    private string easyEnding = "Thanks for playing and beating 'It Ends'.  I admire your resolve in getting to the end.    The path out of the pit was never meant to be easy.";
    private string hardEnding = "Thanks for playing and beating 'It Ends'.  Reaching the end on challenge mode is an impressive feat.  Thanks for your patience, your resolve, and your time.        The path out of the pit was never meant to be easy.";
    // Start is called before the first frame update
    void OnEnable()
    {
        this.GetComponent<Text>().text = "";
        currentText = "";
        entireText = "";
        completionTime = GameEnd.getCompletionTime();
        
        if (PauseMenu.triedEasy())
        {
            entireText += "Completed in " + completionTime + " - Easier mode\n\n" + easyEnding;
            easyFinish = true;
        }
        else
        {
            entireText += "Completed in " + completionTime + " - Default (Challenge mode)\n\n" + hardEnding;
            easyFinish = false;
        }
        StartCoroutine(showText());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator showText()
    {
        foreach (char c in entireText)
        {
            currentText += c;
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
