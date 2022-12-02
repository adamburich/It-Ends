using UnityEngine;

// The Audio Source component has an AudioClip option.  The audio
// played in this example comes from AudioClip and is called audioData.

[RequireComponent(typeof(AudioSource))]
public class LandingOnWallAudio : MonoBehaviour
{
    public AudioSource audioData;

    void OnEnable()
    {
        audioData.Play(0);
        Debug.Log("started");
    }
}