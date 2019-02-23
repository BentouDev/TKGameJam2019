using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Hard,
        Easy
    };
    
    [Header("References")]
    private PlayerMovement Player;

    public GameMode StartingMode;
    public GameMode CurrentMode { get; private set; }

    [Header("Points")]
    public int StartingPoints = 1000;

    public int OnMissBeatDmg = 50;

    public int OnHitPoints = 50;

    [Header("UI & Fx")]
    public Slider PointsSlider;

    public int Points { get; private set; }

    public UnityEvent OnLoseCallback;
    
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
        Points = StartingPoints;
        CurrentMode = StartingMode;

        FindObjectOfType<MusicController>().Coefficient =
            StartingMode == GameMode.Easy ? 1.0f : 0.0f;
    }

    void Update()
    {
        if (Points <= 0)
        {
            OnLose();
        }

        PointsSlider.value = Points;
    }

    public void ToggleMode()
    {
        if (CurrentMode == GameMode.Easy)
            CurrentMode = GameMode.Hard;
        else
            CurrentMode = GameMode.Easy;
    }

    public void OnMissBeat()
    {
        Points -= OnMissBeatDmg;
    }

    public void OnEnemyHit()
    {
        if (CurrentMode == GameMode.Easy)
            Points += OnHitPoints;
    }

    public void OnWin()
    {
        
    }

    public void OnLose()
    {
        OnLoseCallback.Invoke();
    }
}
