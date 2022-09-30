using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GunHUD : GunModeObject
{
    public Image healthFill;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI ammoText;

    private Player _player;

    protected override void Start()
    {
        base.Start();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        healthFill.fillAmount = _player.health / _player.maxHealth;
        healthText.text = ((int)_player.health).ToString();
        ammoText.text = _player.GetComponent<PlayerGunController>().ammo.ToString();
    }
}
