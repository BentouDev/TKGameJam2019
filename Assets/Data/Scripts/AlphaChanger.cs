using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class AlphaChanger : MonoBehaviour, IModeChanged
{
    public bool IsEasy;

    public float Duration = 1;
    
    private List<SpriteRenderer> Sprites = new List<SpriteRenderer>();

    void Start()
    {
        Sprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    public void SetAlpha(float value)
    {
        foreach (var sprite in Sprites)
        {
            var col = sprite.color;
            col.a = value;
            sprite.color = col;
        }  
    }

    public void OnEasy()
    {
        if (IsEasy)
            StartCoroutine(CoAnimate(0.0f, 1.0f));
        else
            StartCoroutine(CoAnimate(1.0f, 0.0f));
    }

    public void OnHard()
    {
        if (!IsEasy)
            StartCoroutine(CoAnimate(0.0f, 1.0f));
        else
            StartCoroutine(CoAnimate(1.0f, 0.0f));
    }

    IEnumerator CoAnimate(float from, float target)
    {
        float elapsed = 0;

        while (elapsed < Duration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Lerp(from, target, elapsed / Duration));
            yield return null;
        }

        SetAlpha(target);
    }
}
