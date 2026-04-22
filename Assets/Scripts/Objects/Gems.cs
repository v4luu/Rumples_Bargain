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

            if (HUDManager.Instance != null)
            {
                HUDManager.Instance.UpdateGemas();

                // actualiza collar si ya tiene todas las gemas
                if (PlayerStats.gemCount >= 5)
                    HUDManager.Instance.UpdateCollar(true);
            }

            AudioSource.PlayClipAtPoint(gemSound, transform.position);
            Destroy(gameObject);
        }
    }
}
