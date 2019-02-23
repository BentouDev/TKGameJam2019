using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeTrigger : MonoBehaviour
{
    private GameManager Game;
    private MusicController Music;
    public float AnimDuration = 3;
    public float AnimDelayed = 0.5f;

    protected static bool CanAnimate;

    // Start is called before the first frame update
    void Start()
    {
        Game = FindObjectOfType<GameManager>();
        Music = FindObjectOfType<MusicController>();
        CanAnimate = true;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanAnimate)
            return;
        
        CanAnimate = false;

        StartCoroutine(CoAnimateModeChange());
        Game.ToggleMode();
    }

    IEnumerator CoAnimateModeChange()
    {
        yield return new WaitForSeconds(AnimDelayed);
        
        float source = Music.Coefficient;
        float target = 1 - source;
        
        float elapsed = 0;
        while (elapsed < AnimDuration)
        {
            elapsed += Time.deltaTime;

            Music.Coefficient = Mathf.Lerp(source, target, elapsed / AnimDuration);

            yield return null;
        }

        Music.Coefficient = target;
        
        yield return new WaitForSeconds(AnimDuration);

        CanAnimate = true;
    }
}
