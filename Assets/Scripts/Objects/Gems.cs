using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int value = 1;
    public AudioClip gemSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.gemCount += value;

            // Reproduce el sonido aunque el objeto se destruya
            AudioSource.PlayClipAtPoint(gemSound, transform.position);

            Destroy(gameObject);
        }
    }
}
