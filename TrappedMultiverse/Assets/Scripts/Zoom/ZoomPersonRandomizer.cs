using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ZoomPersonRandomizer : MonoBehaviour
{
    public Image livePicture;
    public TextMeshProUGUI nameText;
    public List<string> randomNames;
    public List<Sprite> randomFaces;
    public Vector2 pictureChangeInterval = new Vector2(2, 6);
    
    private void OnEnable()
    {
        nameText.text = randomNames[Random.Range(0, randomNames.Count)];
        if (livePicture != null) StartCoroutine(ChangeFaceRoutine());
    }

    private IEnumerator ChangeFaceRoutine()
    {
        livePicture.sprite = randomFaces[Random.Range(0, randomFaces.Count)];
        yield return new WaitForSeconds(Random.Range(pictureChangeInterval.x, pictureChangeInterval.y));
        StartCoroutine(ChangeFaceRoutine());
    }
}
