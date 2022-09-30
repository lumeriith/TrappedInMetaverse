using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct ChatMessage
{
    public string author;
    public string message;
}

public class UI_ChatManager : MonoBehaviour
{
    [NonSerialized]
    public List<ChatMessage> newMessages = new List<ChatMessage>();
    public UI_ChatItem itemPrefab;
    public InputField field;
    public GameObject chatBox;
    
    private List<string> _lastId = new List<string>();
    private float _lastCheckTime;

    private void Awake()
    {
        FirebaseManager.instance.onLoggedInChanged += (isLoggedIn) =>
        {
            if (!isLoggedIn) return;
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            Query query = db.Collection("chat").OrderByDescending("creationDate").Limit(10);

            var listener = query.Listen(snapshot =>
            {
                lock (newMessages)
                {
                    if (_lastId.Count == 0)
                    {
                        foreach (DocumentSnapshot doc in snapshot.Documents)
                            _lastId.Add(doc.Id);
                        return;
                    }
                    
                    foreach (DocumentSnapshot doc in snapshot.Documents)
                    {
                        if (_lastId.Contains(doc.Id)) continue;
                        newMessages.Add(new ChatMessage
                        {
                            author = doc.GetValue<string>("author"),
                            message = doc.GetValue<string>("message")
                        });
                    }
                }
                
                _lastId.Clear();
                foreach (DocumentSnapshot doc in snapshot.Documents)
                    _lastId.Add(doc.Id);
            });
        };
    }

    private IEnumerator SelectRoutine()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // Dirty hack
        for (int i = 0; i < 3; i++)
        {
            field.Select();
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (chatBox.activeSelf)
            {
                Send();
            }
            else
            {
                chatBox.SetActive(true);
                StartCoroutine(SelectRoutine());
            }
        }
        
        if (Time.time - _lastCheckTime > 0.1f)
        {
            _lastCheckTime = Time.time;
            lock (newMessages)
            {
                foreach (var m in newMessages)
                {
                    var msg = Instantiate(itemPrefab, transform);
                    msg.author = m.author;
                    msg.message = m.message;
                }
                newMessages.Clear();
            }
        }
    }

    public void Send()
    {
        chatBox.SetActive(false);
        var msg = field.text.Trim();
        if (msg.Length == 0) return;
        field.text = "";
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("chat").Document();
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "author", FirebaseManager.instance.userName },
            { "creationDate", Timestamp.GetCurrentTimestamp() },
            { "message", msg }
        };
        docRef.SetAsync(data);
    }
}
