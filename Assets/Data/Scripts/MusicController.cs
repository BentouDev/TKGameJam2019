using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public enum NiceProgress
    {
        Yes,
        No
    };
    
    [Header("NOTE: Edit volume in mixer, not in source!!! Bad 0 - 1 Good")]
    public AudioSource[] GoodMusic;
    public AudioSource[] BadMusic;

    public float MusicDelay;
    
    [Range(0,1)]
    public float Coefficient;

    public NiceProgress Progress;
    
    private void Start()
    {
        Progress = NiceProgress.Yes;
    }

    void Update()
    {
        if (!GoodMusic.Any() || !BadMusic.Any())
            return;

        switch (Progress)
        {
            case NiceProgress.Yes:
                
                GoodMusic[(int) NiceProgress.No].volume = 0;
                BadMusic[(int) NiceProgress.No].volume = 0;
                break;
            case NiceProgress.No:
                
                GoodMusic[(int) NiceProgress.Yes].volume = 0;
                BadMusic[(int) NiceProgress.Yes].volume = 0;
                break;
        }

        GoodMusic[(int) Progress].volume = Coefficient;
        BadMusic[(int) Progress].volume = 1.0f - Coefficient;
    }

    public void Play()
    {
        for (int i = 0; i < 2; i++)
        {
            GoodMusic[i].PlayScheduled(MusicDelay);
            BadMusic[i].PlayScheduled(MusicDelay);    
        }

        GoodMusic[1].volume = 0;
        BadMusic[1].volume = 0;
    }

    public float GetTime()
    {
        if (GoodMusic[(int) Progress].volume > 0.5f)
            return GoodMusic[(int) Progress].time;
        return BadMusic[(int) Progress].time;
    }
}
