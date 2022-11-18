using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sound;
    void Start()
    {
        sound.Play();
    }

}
