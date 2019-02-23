﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("NOTE: Edit volume in mixer, not in source!!!")]
    public AudioSource GoodMusic;
    public AudioSource BadMusic;
    
    [Range(0,1)]
    public float Coefficient;

    void Update()
    {
        if (!GoodMusic || !BadMusic)
            return;
        
        GoodMusic.volume = Coefficient;
        BadMusic.volume = 1.0f - Coefficient;
    }

    public void Play()
    {
        if (GoodMusic)
            GoodMusic.Play();

        if (BadMusic)
            BadMusic.Play();
    }
}
