using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float Score;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
