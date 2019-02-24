using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityTemplateProjects;

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
    public UnityEvent OnWonCallback;

    public UnityEvent OnModeEasy;
    public UnityEvent OnModeHard;
    
    private bool Ended;
    
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
        if (Ended)
            return;
        
        if (Points <= 0)
        {
            OnLose();
        }

        PointsSlider.value = Points;
    }

    public void ToggleMode()
    {
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IModeChanged>();
        
        if (CurrentMode == GameMode.Easy)
        {
            CurrentMode = GameMode.Hard;
            OnModeHard.Invoke();
            foreach (IModeChanged s in ss) {
                s.OnHard();
            }
        }
        else
        {
            CurrentMode = GameMode.Easy;        
            OnModeEasy.Invoke();
            foreach (IModeChanged s in ss) {
                s.OnEasy();
            }
        }
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
        Ended = true;
        Player.enabled = false;
        OnWonCallback.Invoke();
        
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IGameEnded>();
        foreach (IGameEnded s in ss) {
            s.OnGameWon();
        }
    }

    public void OnLose()
    {
        Ended = true;
        Player.enabled = false;
        OnLoseCallback.Invoke();
        
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IGameEnded>();
        foreach (IGameEnded s in ss) {
            s.OnGameLost();
        }
    }

    public void WonTheGame()
    {
        OnWin();
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
