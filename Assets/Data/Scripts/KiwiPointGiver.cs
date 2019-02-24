using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiPointGiver : MonoBehaviour
{
    private GameManager Game;
    
    void Start()
    {
        Game = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Game.OnEnemyHit();
        enabled = false;
    }
}
