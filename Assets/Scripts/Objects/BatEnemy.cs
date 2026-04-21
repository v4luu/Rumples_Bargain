using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 2;
    private int currentHealth;

    [Header("Movimiento")]
    public float speed = 2f;
    public float detectionRange = 5f;
    public float patrolRange = 2f;

    [Header("Patrulla")]
    private Vector2 startPosition;
    private Vector2 patrolTarget;
    private bool movingRight = true;

    [Header("Ataque")]
    public int damage = 1;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    private Transform player;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Color originalColor;
    private bool isDead = false;

    public AudioClip damageSound;

    void Start()
    {
        currentHealth = maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = _spriteRenderer.color;

        // agrega AudioSource automaticamente si no existe
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        startPosition = transform.position;
        patrolTarget = startPosition + Vector2.right * patrolRange;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
            ChasePlayer();
        else
            Patrol();
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
        FlipSprite(direction.x);
    }

    void Patrol()
    {
        Vector2 target = movingRight ? patrolTarget : startPosition;
        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            (speed * 0.5f) * Time.deltaTime
        );

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        FlipSprite(direction.x);

        if (Vector2.Distance(transform.position, target) < 0.2f)
            movingRight = !movingRight;
    }

    void FlipSprite(float directionX)
    {
        if (directionX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (damageSound != null)
            _audioSource.PlayOneShot(damageSound);

        StartCoroutine(BlinkRed());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator BlinkRed()
    {
        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                PlayerStats stats = other.GetComponent<PlayerStats>();
                if (stats != null)
                    stats.TakeDamage(damage);
            }
        }
    }
}