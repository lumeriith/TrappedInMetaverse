using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UI_DeathOverlay : MonoBehaviour
{
    public Volume volume;
    public float lerpTime = 2f;
    private void Awake()
    {
        Player.instance.onDeath += () =>
        {
            StartCoroutine(DeathRoutine());
        };
    }

    private void Start()
    {
        StartCoroutine(StartRoutine());
    }

    private IEnumerator StartRoutine()
    {
        for (float v = 0; v < 1f; v += Time.deltaTime / lerpTime)
        {
            volume.weight = 1 - v;
            yield return null;
        }

        volume.weight = 0f;
    }

    private IEnumerator DeathRoutine()
    {
        for (float v = 0; v < 1f; v += Time.deltaTime / lerpTime)
        {
            volume.weight = v;
            yield return null;
        }
    }
}
