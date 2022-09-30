using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarioCoin : MarioModeObject
{
    public Effect pickupEffect;
    public float rotSpeed = 100f;

    protected override void Start()
    {
        base.Start();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
            pickupEffect.PlayNew();
            Player.instance.score += 10;
        }
    }
}
