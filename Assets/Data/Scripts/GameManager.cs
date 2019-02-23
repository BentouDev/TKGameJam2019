using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMovement Player;
    
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        
    }
    
    public void OnWin()
    {
        
    }

    public void OnLose()
    {
        
    }
}
