using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GunDamageFeedback : GunModeObject
{
    public Image vignetteImage;
    public float decaySpeed = 3f;

    private void Awake()
    {
        Player.instance.onTakeDamage += _ =>
        {
            var c = vignetteImage.color;
            c.a = 1f;
            vignetteImage.color = c;
        };
    }

    private void OnEnable()
    {
        var c = vignetteImage.color;
        c.a = 0f;
        vignetteImage.color = c;
    }

    private void Update()
    {
        var c = vignetteImage.color;
        c.a = Mathf.MoveTowards(c.a, 0f, decaySpeed * Time.deltaTime);
        vignetteImage.color = c;
    }
}
