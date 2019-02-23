using System.Collections;
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
        GoodMusic.volume = Coefficient;
        BadMusic.volume = 1.0f - Coefficient;
    }
}
