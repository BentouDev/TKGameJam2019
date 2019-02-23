using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class GUIManager : MonoBehaviour, IGameEnded
{
    public RectTransform WonScreen;
    public RectTransform LostScreen;

    public CanvasGroup Group;

    public float Duration = 0.5f;
    
    delegate void Callback(float value);

    void Start()
    {
        WonScreen.gameObject.SetActive(false);
        LostScreen.gameObject.SetActive(false);
    }

    IEnumerator CoAnimate(Callback callback, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            callback(elapsed / duration);
            yield return null;
        }

        callback(1.0f);
    }

    public void OnGameWon()
    {
        WonScreen.gameObject.SetActive(true);
        StartCoroutine(CoAnimate((value) => { Group.alpha = value; }, Duration));
    }

    public void OnGameLost()
    {
        LostScreen.gameObject.SetActive(true);
        StartCoroutine(CoAnimate((value) => { Group.alpha = value; }, Duration));
    }
}
