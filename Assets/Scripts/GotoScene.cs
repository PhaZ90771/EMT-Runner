using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public uint SceneIndex;

    public void Goto()
    {
        if (SceneIndex == 1) // If goto gameplay scene
        {
            var scoreManager = FindAnyObjectByType<ScoreManager>();
            if (scoreManager)
            {
                Destroy(scoreManager.gameObject); // Delete ScoreManager if it already exists
            }
        }

        SceneManager.LoadScene((int)SceneIndex);
    }
}
