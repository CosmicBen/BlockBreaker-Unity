using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private float paddleSize = 1.0f;
    [SerializeField] private float maxX = 16;
    [SerializeField] private float minX = 0;
    [SerializeField] private Ball ball;

    private GameSession session;

    private void Awake()
    {
        session = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        Vector2 paddlePosition = new Vector2(GetXPos(), transform.position.y);
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        float x;

        if (session.IsAutoplayEnabled())
        {
            x = ball.transform.position.x;
        }
        else
        {
            float screenWidthInUnits = 2.0f * (gameCamera.aspect * gameCamera.orthographicSize);
            x = Mathf.Clamp(screenWidthInUnits * (Input.mousePosition.x / Screen.width) + 0.5f * paddleSize, minX + 0.5f * paddleSize, maxX - 0.5f * paddleSize);
        }

        return x;
    }
}
