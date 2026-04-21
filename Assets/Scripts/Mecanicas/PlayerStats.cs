using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int maxLives = 5;
    public int currentLives;

    public static int coinCount = 0;
    public static int gemCount = 0;
    public static int boxCount = 0;
    public static int wheatCount = 0;

    private bool isInvincible = false;
    private float invincibleDuration = 1.5f;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioSource _audioSource;

    public AudioClip damageSound;
    public GameObject gameOverCanvas; // arrastra el GameOverCanvas aquí

    void Awake()
    {
        Instance = this;
        currentLives = maxLives;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        // asegura que el canvas esté oculto al inicio
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentLives -= amount;

        // actualiza HUD
        if (HUDManager.Instance != null)
            HUDManager.Instance.UpdateVidas();

        if (_audioSource != null && damageSound != null)
            _audioSource.PlayOneShot(damageSound);

        if (_animator != null)
            _animator.SetTrigger("hit");

        if (currentLives <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityFrames());
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        float elapsed = 0f;
        float blinkInterval = 0.1f;

        while (elapsed < invincibleDuration)
        {
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }

        _spriteRenderer.enabled = true;
        isInvincible = false;
    }

    void Die()
    {
        // desactiva controles
        GetComponent<PlayerMove>().enabled = false;
        GetComponent<PlayerJump>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;

        // reproduce animación de muerte
        if (_animator != null)
            _animator.SetTrigger("die");

        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // espera animación de die
        yield return new WaitForSeconds(1f);

        // desaparece el player
        _spriteRenderer.enabled = false;

        // pequeña pausa
        yield return new WaitForSeconds(0.5f);

        // muestra pantalla game over con los botones
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        // ya no recarga automáticamente, el jugador elige
    }
}