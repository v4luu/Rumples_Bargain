using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.coinCount += value;
            
            // Reproduce el sonido aunque el objeto se destruya
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            
            Destroy(gameObject);
        }
    }
}