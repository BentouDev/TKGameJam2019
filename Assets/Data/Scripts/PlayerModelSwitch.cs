using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelSwitch : MonoBehaviour
{
    public Transform EasyModel;
    public Transform HardModel;

    private GameManager Game;
    
    void Start()
    {
        Game = FindObjectOfType<GameManager>();
        if (Game.StartingMode == GameManager.GameMode.Easy)
        {
            EasyModel.gameObject.SetActive(true);
            HardModel.gameObject.SetActive(false);
        }
        else
        {
            EasyModel.gameObject.SetActive(false);
            HardModel.gameObject.SetActive(true);
        }
    }
    
    void Update()
    {
        if (Game.CurrentMode == GameManager.GameMode.Easy)
        {
            if (!EasyModel.gameObject.activeSelf)
                ToggleModel();
        }
        else
        {
            if (!HardModel.gameObject.activeSelf)
                ToggleModel();
        }
    }

    private void ToggleModel()
    {
        var wasActive = EasyModel.gameObject.activeSelf;
        HardModel.gameObject.SetActive( wasActive);
        EasyModel.gameObject.SetActive(!wasActive);
    }
}
