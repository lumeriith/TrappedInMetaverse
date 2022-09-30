using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Action<float> onTakeDamage;
    public Action onDeath;

    public float maxHealth = 100f;
    public float health { get; private set; }

    public bool isAlive => health > 0;
    public bool isDead => !isAlive;

    protected virtual void Awake()
    {
        health = maxHealth;
    }

    public void ApplyDamage(float amount)
    {
        if (isDead) return;
        health = Mathf.MoveTowards(health, 0, amount);
        onTakeDamage?.Invoke(amount);
        if (health <= 0) onDeath?.Invoke();
    }

    public void Kill()
    {
        if (ModeManager.instance.mode != GameMode.Mario) return;
        health = 0;
        onDeath?.Invoke();
    }

    public void Heal()
    {
        health = maxHealth;
    }
}
