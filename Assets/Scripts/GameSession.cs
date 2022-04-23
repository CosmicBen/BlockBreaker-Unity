using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 2.0f)] private float gameSpeed = 1.0f;
    [SerializeField] private int pointsPerBlockDestroyed = 50;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isAutoplayEnabled;

    private int currentScore = 0;
   
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetScoreText();
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsAutoplayEnabled()
    {
        return isAutoplayEnabled;
    }
}
