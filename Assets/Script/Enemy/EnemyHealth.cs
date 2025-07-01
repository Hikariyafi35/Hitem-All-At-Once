using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;  // Kesehatan musuh

    private GameManager gameManager;

    void Start()
    {
        // Mendapatkan referensi ke GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Fungsi untuk mengurangi kesehatan musuh
    public void TakeDamage(float damage)
    {
        health -= damage;  // Mengurangi kesehatan musuh

        // Jika kesehatan musuh <= 0, hancurkan musuh dan beri tahu GameManager
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Beri tahu GameManager bahwa musuh mati
        gameManager.EnemyDied();

        // Hancurkan musuh
        Destroy(gameObject);
    }
}
