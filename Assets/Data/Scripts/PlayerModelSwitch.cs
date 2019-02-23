using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;
using UnityEngine;
using UnityTemplateProjects;

public class PlayerModelSwitch : MonoBehaviour, IModeChanged
{
    public Transform EasyModel;
    public Transform HardModel;

    private GameManager Game;
    
    void Start()
    {
        Game = FindObjectOfType<GameManager>();
        if (Game.StartingMode == GameManager.GameMode.Easy)
        {
            OnEasy();
        }
        else
        {
            OnHard();
        }
    }
    
    void Update()
    {
//        if (Game.CurrentMode == GameManager.GameMode.Easy)
//        {
//            if (!EasyModel.gameObject.activeSelf)
//                ToggleModel();
//        }
//        else
//        {
//            if (!HardModel.gameObject.activeSelf)
//                ToggleModel();
//        }
    }

    private void ToggleModel()
    {
        var wasActive = EasyModel.gameObject.activeSelf;
        HardModel.gameObject.SetActive( wasActive);
        EasyModel.gameObject.SetActive(!wasActive);
    }

    public void OnEasy()
    {
        HardModel.gameObject.SetActive(false);
        EasyModel.gameObject.SetActive(true);
    }

    public void OnHard()
    {
        HardModel.gameObject.SetActive(true);
        EasyModel.gameObject.SetActive(false);
    }
}
