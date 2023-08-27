using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private void Awake()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2f;
    }

    private void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            var offset = startPos.x - transform.position.x;
            var pos = startPos;
            pos.x += repeatWidth - offset;
            transform.position = pos;
        }

    }
}
