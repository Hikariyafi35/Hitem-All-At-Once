using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10f;  // Kesehatan musuh

    // Fungsi untuk mengurangi kesehatan musuh
    public void TakeDamage(float damage)
    {
        health -= damage;  // Mengurangi kesehatan musuh

        // Jika kesehatan musuh <= 0, hancurkan musuh
        if (health <= 0)
        {
            Destroy(gameObject);  // Hancurkan musuh
        }
    }
}
