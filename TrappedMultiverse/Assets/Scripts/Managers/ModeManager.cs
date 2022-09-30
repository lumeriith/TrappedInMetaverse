using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Zoom, Gun, Mario
}

public class ModeManager : ManagerBase<ModeManager>
{
    public Action<GameMode> onModeChanged;
    public GameMode mode { get; private set; }

    private Camera _main;

    private void Start()
    {
        _main = Camera.main;
    }

    private int GetLayerByGameMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.Zoom:
                return Layers.Zoom;
            case GameMode.Gun:
                return Layers.Gun;
            case GameMode.Mario:
                return Layers.Mario;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    public void SetGameMode(GameMode mode)
    {
        if (this.mode == mode) return;
        
        _main.cullingMask &= ~(1 << GetLayerByGameMode(this.mode));
        _main.cullingMask |= 1 << GetLayerByGameMode(mode);
        
        this.mode = mode;
        onModeChanged?.Invoke(mode);
    }

    private void Update()
    {
        if (Player.instance.isDead) return;
        if (UIManager.instance.isInputFieldSelected) return;
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetGameMode(GameMode.Zoom);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetGameMode(GameMode.Gun);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetGameMode(GameMode.Mario);
    }
}
