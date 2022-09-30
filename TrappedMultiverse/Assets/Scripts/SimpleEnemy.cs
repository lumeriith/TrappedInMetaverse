using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleEnemy : Entity
{
    public float gunColRadius;
    public float gunColHeight;
    public float marioColRadius;
    public float marioColHeight;
    public float rotateSpeed = 200f;
    public Effect shootEffect;
    public float shootRpm = 600f;
    public Transform shootPivot;
    public GameObject bulletPrefab;
    public float moveSpeed = 10f;
    public float shootPauseChance = 0.2f;
    public float shootPauseTime = 0.85f;
    public Effect deathEffectGun;
    public Effect deathEffectMario;
    public Renderer[] renderers;
    public float stepOffsetY = 0f;
    public float afterStepVelocity = 5f;

    private CapsuleCollider _col;
    private float _lastCheckTime;
    private float _lastShootTime;
    
    protected override void Awake()
    {
        base.Awake();
        _col = GetComponent<CapsuleCollider>();
        ModeManager.instance.onModeChanged += OnModeChanged;

        onDeath += () =>
        {
            foreach (var r in renderers) r.enabled = false;
            if (ModeManager.instance.mode == GameMode.Mario) deathEffectMario.Play();
            else if (ModeManager.instance.mode == GameMode.Gun) deathEffectGun.Play();
        };
    }

    private void OnModeChanged(GameMode obj)
    {
        if (obj == GameMode.Gun)
        {
            _col.height = gunColHeight;
            _col.radius = gunColRadius;
        } else if (obj == GameMode.Mario)
        {
            _col.height = marioColHeight;
            _col.radius = marioColRadius;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ModeManager.instance.mode != GameMode.Mario) return;
        if (isDead) return;
        if (!collision.collider.TryGetComponent<Player>(out var player)) return;
        if (player.isDead) return;
        if (transform.position.y < player.transform.position.y + stepOffsetY)
        {
            var a = collision.rigidbody.velocity;
            a.y = afterStepVelocity;
            collision.rigidbody.velocity = a;
            Kill();
        }
        else player.Kill();
    }

    private void OnDestroy()
    {
        ModeManager.instance.onModeChanged -= OnModeChanged;
    }

    private bool _isPlayerVisible = false;
    private void Update()
    {
        UpdatePlayerVisibility();
        if (_isPlayerVisible)
        {
            var playerPos = Player.instance.transform.position + Vector3.up * 0.5f;
            var targetRotY = Quaternion.LookRotation(playerPos - transform.position).eulerAngles.y;
            var targetRot = Quaternion.Euler(0, targetRotY, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot,
                rotateSpeed * Time.deltaTime);
            if (ModeManager.instance.mode == GameMode.Mario)
            {
                var nextPos = Vector3.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
                nextPos.y = transform.position.y;
                transform.position = nextPos;
            }
            else if (ModeManager.instance.mode == GameMode.Gun)
            {
                if (Quaternion.Angle(transform.rotation, targetRot) < 10 && Time.time - _lastShootTime > 60f / shootRpm)
                {
                    _lastShootTime = Time.time;
                    if (Random.value < shootPauseChance)
                    {
                        _lastShootTime += shootPauseTime;    
                    }
                    
                    shootEffect.PlayNew();
                    Vector3 startPos = shootPivot.position;
                    Vector3 endPos = playerPos;
                    Instantiate(bulletPrefab, startPos, Quaternion.LookRotation(endPos - startPos));
                }
                
            }
        }
    }

    private void UpdatePlayerVisibility()
    {
        if (isDead)
        {
            _isPlayerVisible = false;
            return;
        }
        
        if (Time.time - _lastCheckTime > 0.2f)
        {
            _lastCheckTime = Time.time;
            var playerPos = Player.instance.transform.position + Vector3.up * 0.5f;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, playerPos - transform.position,
                    (playerPos - transform.position).magnitude - 0.5f, LayerMask.GetMask("World")))
            {
                _isPlayerVisible = false; // Blocked by World
            }
            else
            {
                _isPlayerVisible = true;
            }
        }
    }
}
