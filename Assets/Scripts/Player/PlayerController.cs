using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float magnetRange = 5f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float detectionRadius;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    bool isImmortalMode;
    bool isMagnetMode;
    bool isX2CoinMode;
    bool isFlip;

    public bool IsImmortalMode { get => isImmortalMode; set => isImmortalMode = value; }
    public bool IsMagnetMode { get => isMagnetMode; set => isMagnetMode = value; }
    public bool IsX2CoinMode { get => isX2CoinMode; set => isX2CoinMode = value; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isImmortalMode = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {  
            Flip();
        }

        if (isMagnetMode)
        {
            AttractCoin();
        }
    }

    void Flip()
    {
        isFlip = true;

        SoundManager.Ins.PlaySideSound();

        if (transform.position.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        transform.position = new Vector2(-transform.position.x, transform.position.y);

        KnockbackEnemy();

        StartCoroutine(IsFlip());
    }

    IEnumerator IsFlip()
    {
        yield return new WaitForSeconds(0.1f);
        isFlip = false;
    }

    void KnockbackEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius); 

        foreach (Collider2D col in enemies)
        {
            if (col.CompareTag(TagConsts.ENEMY_TAG))
            {
                SpawnEffectExplosion();

                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Knockback();
                }
            }
        }
    }

    void SpawnEffectExplosion()
    {
        GameObject effect = Instantiate(effectPrefab, effectPrefab.transform.position, Quaternion.identity);
        effect.GetComponent<Animator>().Play("Effect_Explosion");
        Destroy(effect, 0.2f);
    }

    void AttractCoin()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, magnetRange, LayerMask.GetMask("Coin"));
        foreach (Collider2D coin in coins)
        {
            coin.transform.position = Vector2.MoveTowards(coin.transform.position, transform.position, 4f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Ins.isDie == false)
        {
            if (collision.gameObject.tag == TagConsts.OBSTACLE_TAG && isImmortalMode == false)
            {
                SpawnEffectExplosion();

                CineController.Ins.ShakeTrigger();

                Knockback();

                SoundManager.Ins.PlayDieSound();
                GameManager.Ins.EndGame();
            }
            else if(collision.gameObject.tag == TagConsts.OBSTACLE_TAG && isImmortalMode == true)
            {
                SpawnEffectExplosion();

                CineController.Ins.ShakeTrigger();

                Wood woodRb = collision.gameObject.GetComponent<Wood>();
                if (woodRb != null)
                {
                    Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
                    woodRb.KnockBack(forceDirection * 20f); 
                }
            }

            if (collision.gameObject.tag == TagConsts.COIN_TAG)
            {
                SoundManager.Ins.PlayCoinSound();
                Destroy(collision.gameObject);
                GameManager.Ins.UpdateCoin();
            }

            if (collision.gameObject.tag == TagConsts.ENEMY_TAG && isImmortalMode == false)
            {
                if (isFlip) return;

                SpawnEffectExplosion();

                CineController.Ins.ShakeTrigger();

                Knockback();

                SoundManager.Ins.PlayDieSound();
                GameManager.Ins.EndGame();
            }
            else if (collision.gameObject.tag == TagConsts.ENEMY_TAG && isImmortalMode == true)
            {
                SpawnEffectExplosion();

                CineController.Ins.ShakeTrigger();

                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.Knockback();
                } 
            }
        }
    }

    void Knockback()
    {
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;
        float knockbackDirection = transform.position.x > 0 ? 1f : -1f;
        rb.AddForce(new Vector2(knockbackDirection, 1f) * knockbackForce, ForceMode2D.Impulse);
    }

    public void ImmortalMode(bool isShow)
    {
        isImmortalMode = isShow;
    }

    public void MagnetMode(bool isShow)
    {
        isMagnetMode = isShow;
    }

    public void X2CoinMode(bool isShow)
    {
        isX2CoinMode = isShow;
    }
}
