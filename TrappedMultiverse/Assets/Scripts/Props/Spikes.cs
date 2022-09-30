using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Entity
{
    public Effect explodeEffect;
    public float explosionRadius = 8f;
    public float explosionDamage = 50;
    
    protected override void Awake()
    {
        base.Awake();
        onDeath += () =>
        {
            var res = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Entity"));
            foreach (var r in res)
            {
                if (r.TryGetComponent<Entity>(out var victim) && victim.isAlive)
                    victim.ApplyDamage(explosionDamage);
            }

            explodeEffect.PlayNew();
            Destroy(gameObject);
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Player>(out var player)) return;
        if (player.isDead) return;
        
        if (ModeManager.instance.mode == GameMode.Gun)
        {
            Kill();

        }
        else if (ModeManager.instance.mode == GameMode.Mario)
        {
            player.Kill();
        }
    }
}
