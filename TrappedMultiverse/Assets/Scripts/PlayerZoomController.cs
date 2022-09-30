using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoomController : ZoomModeScript
{
    private FirstPersonMovement _movement;
    private Jump _jump;
    private Rigidbody _rb;


    private void Awake()
    {
        _movement = GetComponent<FirstPersonMovement>();
        _jump = GetComponent<Jump>();
        _rb = GetComponent<Rigidbody>();
        _savedSpeed = _movement.speed;
        _savedRunSpeed = _movement.runSpeed;
    }


    private float _savedSpeed;
    private float _savedRunSpeed;

    private void OnEnable()
    {
        _jump.enabled = false;
        _movement.speed = 0;
        _movement.runSpeed = 0;
        _rb.isKinematic = true;
        var player = GetComponent<Player>();
        player.Heal();
    }

    private void OnDisable()
    {
        _jump.enabled = true;
        _movement.speed = _savedSpeed;
        _movement.runSpeed = _savedRunSpeed;
        _rb.isKinematic = false;
    }
}
