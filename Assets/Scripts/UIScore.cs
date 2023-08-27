using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TMP_Text textObj;
    private ScoreManager scoreManager;

    private void Awake()
    {
        textObj = GetComponent<TMP_Text>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    private void Update()
    {
        textObj.text = "Score: " + scoreManager.Score.ToString("N2");
    }
}
