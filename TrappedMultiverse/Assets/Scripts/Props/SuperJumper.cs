using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumper : MarioModeObject
{
    public float jumpVelocity = 8f;
    public Effect jumpEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (ModeManager.instance.mode != GameMode.Mario) return;
        if (!collision.collider.TryGetComponent<Player>(out var player)) return;
        if (player.isDead) return;
        var a = collision.rigidbody.velocity;
        a.y = jumpVelocity;
        collision.rigidbody.velocity = a;
        jumpEffect.Play();
    }
}
