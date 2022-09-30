using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChatItem : MonoBehaviour
{
    public string author;
    public string message;

    public Text authorText;
    public Text messageText;
    
    private void Start()
    {
        authorText.text = author;
        messageText.text = message;
        Destroy(gameObject, 8f);
    }
}
