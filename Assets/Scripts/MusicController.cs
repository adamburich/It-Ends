using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource music;
    public Rigidbody2D player;

    public void Update()
    {
        float completed = player.position.y / 153;
        music.pitch = .85f + completed / 4;
    }
}
