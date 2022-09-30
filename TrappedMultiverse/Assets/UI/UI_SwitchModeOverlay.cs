using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UI_SwitchModeOverlay : MonoBehaviour
{
    public Volume volume;
    public float lerpTime = 2f;
    public AudioSource source;
    private void Awake()
    {
        ModeManager.instance.onModeChanged += _ =>
        {
            StartCoroutine(SwitchRoutine());
        };
    }

    private IEnumerator SwitchRoutine()
    {
        source.Play();
        for (float v = 0; v < 1f; v += Time.deltaTime / lerpTime)
        {
            volume.weight = 1 - v;
            yield return null;
        }
    }
}
