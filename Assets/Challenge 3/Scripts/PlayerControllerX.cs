using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce = 75f;
    public float bounceForce = 75f;
    private readonly float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    private bool bouncing = false;

    private readonly float maxHeight = 14f;
    private readonly float minHeight = 1.5f;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    private void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    private void Update()
    {
        var height = transform.position.y;
        var inBounds = height <= maxHeight && height >= minHeight;
        var holdingSpace = Input.GetKey(KeyCode.Space);

        // While space is pressed and player is low enough, float up
        if (!gameOver && inBounds && holdingSpace)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (inBounds)
        {
            bouncing = false;
        }
        if (height <= minHeight)
        {
            playerRb.AddForce(Vector3.up * bounceForce);
            if (!bouncing)
            {
                playerAudio.PlayOneShot(bounceSound, 1.0f);
                bouncing = true;
            }
        }
        if (height >= maxHeight)
        {
            playerRb.AddForce(Vector3.down * bounceForce);
            if (!bouncing)
            {
                playerAudio.PlayOneShot(bounceSound, 1.0f);
                bouncing = true;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
