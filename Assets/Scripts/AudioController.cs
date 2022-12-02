using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public AudioSource slide;
    public AudioSource land;
    public PlayerController controller;
    private float vertSpeed;

    public void Update()
    {
        if(vertSpeed != 0 && controller.getVerticalSpeed() == 0)
        {
            land.Play();
        }
        if (controller.wallSliding() && !controller.isGrounded()) {
            if (!slide.isPlaying)
            {
                slide.Play();
            }
        }
        else
        {
            slide.Stop();
        }

        vertSpeed = controller.getVerticalSpeed();
    }
}