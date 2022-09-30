using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ZoomWindowObject : MonoBehaviour
{
    private Transform _playerCameraTransform;
    private void Awake()
    {
        _playerCameraTransform = Player.instance.GetComponentInChildren<CinemachineVirtualCamera>().transform;
    }

    private void OnEnable()
    {
        transform.position = _playerCameraTransform.position;
        transform.rotation = Quaternion.Euler(0, Player.instance.transform.rotation.eulerAngles.y, 0);
    }

    private void Start()
    {
        transform.position = _playerCameraTransform.position;
        transform.rotation = Quaternion.Euler(0, Player.instance.transform.rotation.eulerAngles.y, 0);
    }
}
