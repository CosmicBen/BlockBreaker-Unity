using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 2.0f;
    [SerializeField] private float yPush = 15.0f;
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] private float randomFactor = 0.2f;

    private Vector2 paddleOffset;
    private bool hasStarted = false;
    private AudioSource audioSource;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        paddleOffset = paddle1.transform.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        if (!hasStarted)
        {
            LockToPaddle();
            LaunchOnClick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            audioSource.PlayOneShot(ballSounds[Random.Range(0, ballSounds.Length)]);
            rigidbody2d.velocity += new Vector2(Random.Range(0.0f, randomFactor), Random.Range(0.0f, randomFactor));
        }
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody2d.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockToPaddle()
    {
        transform.position = paddle1.transform.TransformPoint(paddleOffset);
    }
}
