using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;  // Kesehatan musuh
    private bool recentlyHit = false;
    private GameManager gameManager;

    void Start()
    {
        // Mendapatkan referensi ke GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Fungsi untuk mengurangi kesehatan musuh
    public void TakeDamage(float damage)
    {
        if (recentlyHit) return;

        health -= damage;
        recentlyHit = true;
        StartCoroutine(ResetHit());
        
        if (health <= 0)
        {
            Die();
        }
    }
    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.05f);
        recentlyHit = false;
    }

    void Die()
    {
        // Beri tahu GameManager bahwa musuh mati
        gameManager.EnemyDied();

        // Hancurkan musuh
        Destroy(gameObject);
    }
}
