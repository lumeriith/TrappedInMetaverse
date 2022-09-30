using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float lerpTime = 1f;
    public float lerpOffset = 0f;
    public Transform endPosition;
    
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(_startPosition, endPosition.position, Mathf.PingPong(Time.time / lerpTime + lerpOffset, 1));
    }
}
