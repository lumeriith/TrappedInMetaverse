using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModeObject : MonoBehaviour
{
    protected abstract GameMode mode { get; }
    
    protected virtual void Start()
    {
        ModeManager.instance.onModeChanged += ReactModeChange;
        if (ModeManager.instance.mode != mode) gameObject.SetActive(false);
    }
    
    protected virtual void OnDestroy()
    {
        if (ModeManager.instance != null)
            ModeManager.instance.onModeChanged -= ReactModeChange;
    }
    
    private void ReactModeChange(GameMode newMode)
    {
        gameObject.SetActive(newMode == mode);
    }
}
