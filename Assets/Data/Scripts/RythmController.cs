using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class RythmController : MonoBehaviour, IGameEnded
{
    [Header("Debug")]
    public bool DrawDebug;

    [Header("Music")]
    public MusicController Controller;
    
    public int BPM = 120;

    [Range(0.0f, 1.0f)]
    public float ErrorMargin = 0.25f;
    
    [Header("0 - nothing, # - skill usage required")]
    public string BeatMap;

    public float StepTime
    {
        get { return 60.0f / (float) BPM; }
    }

    private int BeatIndex = 0;
    private float LastBeatTime;

    public delegate void AfterBeatDelegate();
    
    public event AfterBeatDelegate OnAfterBeat;
    public event AfterBeatDelegate OnAfterPause;

    private bool IsPlaying = false;

    [Header("Display")]
    public RectTransform BeatTrack; 
    public float BeatElementSize = 150;
    private float BeatOffset;
    public Sprite BlankSprite;
    public Sprite BeatSprite;

    public Animator SelectorAnimator;

    public string OnHit = "OnHit";
    public string OnMiss = "OnMiss";
    public string OnReady = "OnReady";

    public Image TargetFrame;
    private List<Image> TrackImages = new List<Image>();
    
    public float BeatVelocity
    {
        get { return BeatElementSize / (StepTime / 2.0f); }
    }
    
    private void OnValidate()
    {
        if (BeatMap.Length == 0)
            BeatMap = "0";
    }

    float GetTime()
    {
        return Controller.GetTime();
    }

    private float LastTime;
    
    void UpdateBeatTrack(float deltaTime)
    {
        BeatOffset -= BeatVelocity * deltaTime;

        var margin = BeatElementSize * BeatMap.Length;
        if (BeatOffset <= -margin)
            BeatOffset = 0; // save delta!

        BeatTrack.anchoredPosition = new Vector2(BeatOffset, 0);
        BeatTrack.ForceUpdateRectTransforms();
    }

    void Start()
    {
        Controller.Play();
        
        TrackImages.AddRange(BeatTrack.GetComponentsInChildren<Image>());
        for (int i = 0; i < TrackImages.Count; i++)
        {
            TrackImages[i].sprite = BeatMap[i % BeatMap.Length] == '#' ? BeatSprite : BlankSprite;
        }
    }

    void SwapImages()
    {
        for (int index = 0; index < TrackImages.Count - 1; index++)
        {
            TrackImages[index].sprite = BeatMap[(index + 1) % BeatMap.Length] == '#' ? BeatSprite : BlankSprite;
        }
    }

    void Update()
    {
        var deltaTime = GetTime() - LastTime;
        var elapsed = GetTime() - LastBeatTime;
        if (elapsed > StepTime)
        {
            LastBeatTime = GetTime();

            if (IsInSkillFrame() && OnAfterBeat != null)
            {
                OnAfterBeat.Invoke();
                var ss = FindObjectsOfType<MonoBehaviour>().OfType<IBeatHandler>();
                foreach (IBeatHandler s in ss) {
                    s.OnBeat();
                }
            }
            else if (OnAfterPause != null)
                OnAfterPause.Invoke();
            
            BeatIndex++;
            
            // SwapImages();
        }

        if (BeatIndex >= BeatMap.Length)
            BeatIndex = 0;

        // var oldSkill = _isInSkillFrame;
        _isInSkillFrame = GetSkillStatus(elapsed);
        
//        if (_isInSkillFrame && !oldSkill)
//            SelectorAnimator.SetTrigger(OnReady);
        
        if (_isInSkillFrame)
            TargetFrame.color = Color.red;
        else
            TargetFrame.color = Color.white;
        
        UpdateBeatTrack(deltaTime);
        LastTime = GetTime();
    }

    private bool _isInSkillFrame;

    public void Miss()
    {
        // SelectorAnimator.SetTrigger(OnMiss);
    }

    public void Hit()
    {
        // SelectorAnimator.SetTrigger(OnHit);
    }

    private bool GetSkillStatus(float elapsed)
    {
        if (BeatMap[BeatIndex] == '#')
            return true;

        int next = BeatIndex + 1;
        if (next >= BeatMap.Length)
            next = 0;

        int prev = BeatIndex - 1;
        if (prev < 0)
            prev = BeatMap.Length - 1;
        
        // BEAT ASSIST (tm)!!!
        if (BeatMap[next] == '#' && elapsed > (1.0f - ErrorMargin) * StepTime)
            return true;

        if (BeatMap[prev] == '#' && elapsed < ErrorMargin * StepTime)
            return true;

        return false;
    }

    public bool IsInSkillFrame()
    {
        return _isInSkillFrame;
    }

    void OnGUI()
    {
        if (!DrawDebug)
            return;
        
        GUI.Label(new Rect(200,10,300,30), "frame time (sec): " + StepTime + "; beatIndex: " + BeatIndex);
        GUI.Label(new Rect(200,50,300,30), "beat offset: " + BeatOffset);

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

    public void OnGameWon()
    {
        CallTheStop();
    }

    public void OnGameLost()
    {
        CallTheStop();
    }

    void CallTheStop()
    {
        
    }
}
