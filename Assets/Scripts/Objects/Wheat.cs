using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    public int value = 1;
    public AudioClip wheatSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.wheatCount += value;

            // Reproduce el sonido aunque el objeto se destruya
            AudioSource.PlayClipAtPoint(wheatSound, transform.position);

            Destroy(gameObject);
        }
    }
}
