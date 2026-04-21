using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int value = 1;
    public AudioClip boxSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.boxCount += value;

            // Reproduce el sonido aunque el objeto se destruya
            AudioSource.PlayClipAtPoint(boxSound, transform.position);

            Destroy(gameObject);
        }
    }
}
