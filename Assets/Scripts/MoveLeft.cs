using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;
    private readonly float baseSpeed = 20f;
    private readonly float leftBound = -15f;

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        var speed = baseSpeed;
        if (playerController.IsDashing)
        {
            speed *= playerController.DashModifier;
        }
        if (playerController.IsPlaying)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
