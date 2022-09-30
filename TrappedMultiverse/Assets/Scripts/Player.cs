using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public static Player instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Player>();
            return _instance;
        }
    }

    private static Player _instance;

    public static Vector3? savePoint;
    public static Quaternion? saveRotation;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        savePoint = null;
        saveRotation = null;
    }

    public Effect marioDeathEffect;
    public Effect gunDeathEffect;
    public Action onSave;
    
    
    public int score = 0;

    public void Save(Vector3 position)
    {
        savePoint = position;
        saveRotation = transform.rotation;
        onSave?.Invoke();
    }
    
    protected override void Awake()
    {
        base.Awake();
        onDeath += () =>
        {
            StartCoroutine(DeathRoutine());
        };
        if (savePoint.HasValue)
        {
            transform.position = savePoint.Value;
            transform.rotation = saveRotation.Value;
        }
    }

    private IEnumerator DeathRoutine()
    {
        if (ModeManager.instance.mode == GameMode.Mario)
            marioDeathEffect.Play();
        else if (ModeManager.instance.mode == GameMode.Gun)
            gunDeathEffect.Play();
        GetComponent<FirstPersonMovement>().enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
