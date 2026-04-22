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

            if (HUDManager.Instance != null)
                HUDManager.Instance.UpdatePaja();

            AudioSource.PlayClipAtPoint(wheatSound, transform.position);
            Destroy(gameObject);
        }
    }
}
