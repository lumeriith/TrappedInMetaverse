using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : GunModeScript
{
    public int ammo;
    public int maxAmmo;
    
    public float reloadTime = 1.5f;
    public float shootAnimTime = 0.5f;

    public GameObject bulletPrefab;
    public Transform cameraTransform;
    public Transform bulletStartTransform;
    
    [Header("Controls")]

    public KeyCode reloadKey = KeyCode.R;
    public KeyCode shootKey = KeyCode.Mouse0;


    
    [Header("Effects")]
    public Effect shootEffect;
    public Effect emptyEffect;
    public Effect reloadEffect;
    
    public float roundsPerMin = 300f;
    
    [Header("Animations")]
    public GameObject gunModel;
    public Vector3 shootPosOffset;
    public Vector3 shootRotOffset;
    public Vector3 reloadOffset;
    public Vector3 reloadRotOffset;

    
    private float _lastShootTime;
    private bool _isReloading;
    
    private void Update()
    {
        if (UIManager.instance.isInputFieldSelected) return;

        if (Input.GetKeyDown(shootKey) && ammo <= 0)
        {
            emptyEffect.PlayNew();
        }

        if (Input.GetKey(shootKey) && Time.time - _lastShootTime > 60f / roundsPerMin && ammo > 0 && !_isReloading)
        {
            ammo--;
            _lastShootTime = Time.time;
            shootEffect.PlayNew();
            Vector3 startPos = bulletStartTransform.position;
            Vector3 endPos = bulletStartTransform.position + cameraTransform.forward;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var res, 100f,
                    LayerMask.GetMask("Entity", "World", "Gun"))) endPos = res.point;
            Instantiate(bulletPrefab, startPos, Quaternion.LookRotation(endPos - startPos));
        }

        if (Input.GetKeyDown(reloadKey) && ammo < maxAmmo && !_isReloading)
        {
            StartCoroutine(ReloadRoutine());
        }

        if (!_isReloading)
        {
            gunModel.transform.localPosition = Vector3.Lerp(shootPosOffset, Vector3.zero,
                (Time.time - _lastShootTime) / shootAnimTime);
            gunModel.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(shootRotOffset), Quaternion.identity,
                (Time.time - _lastShootTime) / shootAnimTime);
        }
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        reloadEffect.Play();
        for (float v = 0; v < 1; v += Time.deltaTime / (reloadTime / 3f * 2))
        {
            gunModel.transform.localPosition = Vector3.Lerp(Vector3.zero, reloadOffset, v);
            gunModel.transform.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(reloadRotOffset), v);
            yield return null;
        }
        for (float v = 0; v < 1; v += Time.deltaTime / (reloadTime / 3f))
        {
            gunModel.transform.localPosition = Vector3.Lerp(Vector3.zero, reloadOffset, 1 - v);
            gunModel.transform.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(reloadRotOffset), 1 - v);
            yield return null;
        }

        ammo = maxAmmo;
        _isReloading = false;
    }
}
