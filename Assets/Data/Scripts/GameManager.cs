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
    public int OnGoodBeatPoints = 10;

    [Header("UI & Fx")]
    public Slider PointsSlider;

    public int Points { get; private set; }

    public UnityEvent OnMissEasy;
    public UnityEvent OnMissHard;
    
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

        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IModeChanged>();
        if (StartingMode == GameMode.Easy)
        {
            foreach (IModeChanged s in ss) {
                s.OnEasy();
            }
        }
        else
        {
            foreach (IModeChanged s in ss) {
                s.OnHard();
            }
        }
    }

    void Update()
    {
        if (Ended)
            return;
        
        if (Points <= 0)
        {
            OnLose();
        }
        
        if (Points > 1500)
            Debug.DebugBreak();

        Points = Mathf.Clamp(Points, 0, 2000);
        PointsSlider.value = Points;
    }

    public void ToggleMode()
    {
        if (Ended)
            return;

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
        if (Ended)
            return;
        
        Points -= OnMissBeatDmg;
        if (CurrentMode == GameMode.Easy)
            OnMissEasy.Invoke();
        else
            OnMissHard.Invoke();
    }

    public void OnEnemyHit()
    {
        if (Ended)
            return;

        //if (CurrentMode == GameMode.Easy)
        Points += OnHitPoints;
    }

    public void OnGoodBeat()
    {
        if (Ended)
            return;

        Points += OnGoodBeatPoints;
    }

    public void OnWin()
    {
        Ended = true;
        Player.IsAlive = false;
        OnWonCallback.Invoke();
        
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IGameEnded>();
        foreach (IGameEnded s in ss) {
            s.OnGameWon();
        }
    }

    public void OnLose()
    {
        Ended = true;
        Player.IsAlive = false;
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
