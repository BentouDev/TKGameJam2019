using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RythmController : MonoBehaviour
{
    public bool DrawDebug;

    public MusicController Controller;
    
    public int BPM = 120;
    
    [Header("0 - nothing, # - skill usage required")]
    public string BeatMap;

    public float FrameTime
    {
        get { return 60.0f / (float) BPM; }
    }

    private int BeatIndex = 0;
    private float LastBeatTime;

    public delegate void AfterBeatDelegate();
    
    public event AfterBeatDelegate OnAfterBeat;
    public event AfterBeatDelegate OnAfterPause;

    private bool IsPlaying = false;
    
    private void OnValidate()
    {
        if (BeatMap.Length == 0)
            BeatMap = "0";
    }

    void Update()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
            Controller.Play();
        }
        
        if (Time.time - LastBeatTime > FrameTime)
        {
            LastBeatTime = Time.time;

            if (IsInSkillFrame() && OnAfterBeat != null)
                OnAfterBeat.Invoke();
            else if (OnAfterPause != null)
                OnAfterPause.Invoke();
            
            BeatIndex++;
        }

        if (BeatIndex >= BeatMap.Length)
            BeatIndex = 0;
    }

    public bool IsInSkillFrame()
    {
        return BeatMap[BeatIndex] == '#';
    }

    void OnGUI()
    {
        if (!DrawDebug)
            return;
        
        GUI.Label(new Rect(200,10,300,30), "frame time (sec): " + FrameTime + "; beatIndex: " + BeatIndex);

        string output = string.Empty;
        for (int i = 0; i < BeatMap.Length; i++)
        {
            int index = BeatIndex + i;
            if (index >= BeatMap.Length)
                index -= BeatMap.Length;
            output += BeatMap[index].ToString();
        }
        
        GUI.Label(new Rect(200,30,200,30), output);
    }
}
