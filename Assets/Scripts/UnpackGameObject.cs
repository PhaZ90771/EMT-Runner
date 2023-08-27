using UnityEngine;

public class UnpackGameObject : MonoBehaviour
{
    private void Start()
    {
        while (transform.childCount > 0)
        {
            transform.GetChild(0).parent = null;
        }
        Destroy(gameObject);
    }
}
