using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public Entity owner;
    public float angleDeviation;
    public float damage = 10;
    public Effect landEffect;
    public Effect hitEffect;

    public float bulletSpeed = 200f;
    public float lifeTime = 5f;

    private float _startTime;
    private Vector3 _lastPosition;
    
    private void Start()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(Random.insideUnitCircle * angleDeviation);
        _startTime = Time.time;
        _lastPosition = transform.position;
    }

    private void Update()
    {
        if (Time.time - _startTime > lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        var oldPos = transform.position;
        var newPos = transform.position + transform.forward * (Time.deltaTime * bulletSpeed);
        transform.position = newPos;

        if (Physics.Raycast(oldPos, newPos - oldPos, out var hit, Vector3.Distance(oldPos, newPos),
                LayerMask.GetMask("Entity", "World", "Gun")))
        {
            Entity target = hit.collider.GetComponent<Entity>();
            if (owner != null && owner == target) return;
            if (target != null && target.isDead) return;
            if (target != null)
            {
                if (target.isDead) return;
                target.ApplyDamage(damage);
                Destroy(gameObject);
                hitEffect.PlayNew(hit.point, Quaternion.identity);
            }
            else
            {
                Destroy(gameObject);
                landEffect.PlayNew(hit.point, Quaternion.identity);
            }
        }
    } 
}
