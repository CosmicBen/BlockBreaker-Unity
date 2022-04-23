using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private float audioVolume = 0.1f;
    [SerializeField] private GameObject blockSparklesVfx;
    [SerializeField] private int maxHits = 1;
    [SerializeField] private Sprite[] hitSprits;

    private Camera mainCamera;
    private Level level;
    private GameSession gameStatus;
    private SpriteRenderer spriteRenderer;

    private int hits;

    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        level = FindObjectOfType<Level>();
    }

    private void Start()
    {
        if (IsBreakable)
        {
            gameStatus = FindObjectOfType<GameSession>();
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBreakable)
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        hits++;

        if (hits >= maxHits)
        {
            BlockDestroyed();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = hits - 1;
        if (spriteIndex >= 0 && spriteIndex < hitSprits.Length && hitSprits[spriteIndex] != null)
        {
            spriteRenderer.sprite = hitSprits[spriteIndex];
        }
        else
        {
            Debug.LogError(name + ": Block sprite is missing from array.");
        }
    }

    private void BlockDestroyed()
    {
        gameStatus.AddToScore();
        level.BlockDestroyed();

        AudioSource.PlayClipAtPoint(breakSound, mainCamera.transform.position, audioVolume);
        Destroy(gameObject);

        TriggerSparklesVfx();
    }

    private void TriggerSparklesVfx()
    {
        GameObject sparkles = Instantiate(blockSparklesVfx, transform.position, transform.rotation);
        Destroy(sparkles, 2.0f);
    }

    private bool IsBreakable
    {
        get { return CompareTag("Breakable"); }
    }
}
