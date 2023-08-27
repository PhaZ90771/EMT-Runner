using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public bool GameOver { get; private set; } = false;
    public bool Startup { get; private set; } = true;
    public bool IsPlaying { get => !Startup && !GameOver; }
    public float Score { get; private set; } = 0;
    public bool IsDashing { get; private set; } = false;

    public readonly float DashModifier = 2.0f;

    private readonly float dashScoreModifier = 3.0f;
    private readonly float jumpForce = 600f;
    private readonly float airJumpForce = 300f;
    private readonly float gravityModifier = 1.5f;
    private readonly uint doubleJumps = 1;

    private Animator animator;
    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private bool isOnGround = true;
    private uint doubleJumpsLeft = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    private void Update()
    {
        if (Startup)
        {
            var pos = transform.position;

            pos.x = Mathf.Lerp(pos.x, 0f, Time.deltaTime * 5f);

            transform.position = pos;
        }

        if (Startup && transform.position.x >= -0.25f)
        {
            var pos = transform.position;
            pos.x = 0;
            transform.position = pos;
            Startup = false;
        }

        if (!Startup)
        {
            var scoreChange = Time.deltaTime;
            if (IsDashing) scoreChange *= dashScoreModifier;
            Score += scoreChange;

            var speed = 1f;
            if (IsDashing) speed *= DashModifier;
            animator.SetFloat("Speed_f", speed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isOnGround)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isOnGround = false;
                    animator.SetTrigger("Jump_trig");
                    dirtParticle.Stop();
                    audioSource.PlayOneShot(jumpSound, 1.0f);
                }
                else if (doubleJumpsLeft > 0)
                {
                    rigidbody.AddForce(Vector3.up * airJumpForce, ForceMode.Impulse);
                    doubleJumpsLeft--;
                    animator.SetTrigger("Jump_trig");
                    audioSource.PlayOneShot(jumpSound, 1.0f);
                }
            }
            IsDashing = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            IsDashing = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJumpsLeft = doubleJumps;
            if (!GameOver) dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound, 1.0f);
        }
    }
}
