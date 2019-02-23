using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonTrigger : MonoBehaviour
{
    private GameManager Game;
    public float Delay = 2;

    private void Start()
    {
        Game = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(CoDelay());
    }

    IEnumerator CoDelay()
    {
        yield return new WaitForSeconds(Delay);
        
        Game.WonTheGame();
    }
}
