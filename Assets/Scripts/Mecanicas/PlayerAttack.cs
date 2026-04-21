using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool rockUnlocked = false;
    public GameObject rockProjectilePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;

    void Update()
    {
        if (rockUnlocked && Input.GetKeyDown(KeyCode.S))
            ThrowRock();
    }

    public void UnlockRock()
    {
        rockUnlocked = true;
        Debug.Log("Piedra desbloqueada");
    }

    void ThrowRock()
    {
        bool facingRight = transform.rotation.eulerAngles.y == 0;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        GameObject rock = Instantiate(
            rockProjectilePrefab,
            throwPoint.position,
            Quaternion.identity
        );

        Rigidbody2D rockRb = rock.GetComponent<Rigidbody2D>();
        if (rockRb != null)
            rockRb.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }
}
