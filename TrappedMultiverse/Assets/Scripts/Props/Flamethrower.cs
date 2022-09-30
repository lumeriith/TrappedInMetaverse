using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public Transform endPoint;
    public float damageInterval = 0.25f;
    public float damageAmount = 10f;
    public float sphereCastRadius = 0.25f;
    private float _lastCheckTime;

    private void Update()
    {
        if (Time.time - _lastCheckTime > damageInterval)
        {
            _lastCheckTime = damageInterval;
            var victs = Physics.SphereCastAll(transform.position, sphereCastRadius, endPoint.position - transform.position,
                (endPoint.position - transform.position).magnitude, LayerMask.GetMask("Entity"));
            foreach (var v in victs)
            {
                if (v.collider.TryGetComponent<Player>(out var p))
                {
                    if (ModeManager.instance.mode == GameMode.Gun) 
                        p.ApplyDamage(damageAmount);
                    else if (ModeManager.instance.mode == GameMode.Mario)
                        p.Kill();
                }
            }
        }
    }
}
