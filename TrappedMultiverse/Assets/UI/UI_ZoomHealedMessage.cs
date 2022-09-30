using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ZoomHealedMessage : ZoomModeObject
{
    public float appearTime = 1f;
    public float sustainTime = 1f;
    public float disappearTime = 1f;
    
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartCoroutine(SaveRoutine());
    }

    private IEnumerator SaveRoutine()
    {
        for (float v = 0; v < 1f; v += Time.deltaTime / appearTime)
        {
            _canvasGroup.alpha = v;
            yield return null;
        }
        for (float v = 0; v < 1f; v += Time.deltaTime / sustainTime)
        {
            yield return null;
        }
        for (float v = 0; v < 1f; v += Time.deltaTime / disappearTime)
        {
            _canvasGroup.alpha = 1 - v;
            yield return null;
        }

        _canvasGroup.alpha = 0f;
    }

}
