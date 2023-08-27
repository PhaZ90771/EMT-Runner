using TMPro;
using UnityEngine;

public class UIDashMultiplier : MonoBehaviour
{
    PlayerController controller;
    TMP_Text textObj;

    private void Awake()
    {
        controller = FindAnyObjectByType<PlayerController>();
        textObj = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textObj.enabled = controller.IsDashing;
    }
}
