using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TMP_Text textObj;
    private PlayerController player;

    private void Awake()
    {
        textObj = GetComponent<TMP_Text>();
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        textObj.text = "Score: " + player.Score.ToString();
    }
}
