using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : ManagerBase<UIManager>
{
    public bool isInputFieldSelected { get; private set; }

    private void Update()
    {
        isInputFieldSelected = EventSystem.current.currentSelectedGameObject != null && 
                               EventSystem.current.currentSelectedGameObject.TryGetComponent<InputField>(out _);
    }
}
