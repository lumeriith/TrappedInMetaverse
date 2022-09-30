using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ModeIcon : MonoBehaviour
{
    public GameMode mode;

    public float activeAlpha = 1f;
    public float inactiveAlpha = 0.5f;
    private CanvasGroup _cg;

    private void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
        ModeManager.instance.onModeChanged += OnModeChanged;
    }

    private void Start()
    {
        OnModeChanged(ModeManager.instance.mode);
    }

    private void OnModeChanged(GameMode obj)
    {
        _cg.alpha = obj == mode ? activeAlpha : inactiveAlpha;
    }
}
