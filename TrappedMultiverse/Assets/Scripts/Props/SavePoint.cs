using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SavePoint : MonoBehaviour
{
    public Effect marioEffect;
    public Effect gunEffect;
    
    public float rotSpeed = 100f;

    private void Start()
    {
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
            if (ModeManager.instance.mode == GameMode.Mario) marioEffect.PlayNew();
            else if (ModeManager.instance.mode == GameMode.Gun) gunEffect.PlayNew();
            Player.instance.Save(transform.position + Vector3.up);
        }
    }
}
