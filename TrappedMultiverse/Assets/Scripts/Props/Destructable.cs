using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Entity
{
    public Effect destroyEffect;

    protected override void Awake()
    {
        base.Awake();
        onDeath += () =>
        {
            destroyEffect.PlayNew();
            Destroy(gameObject);
        };
    }
}
