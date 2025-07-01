using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Kecepatan peluru
    private Rigidbody2D rb;  // Rigidbody2D peluru untuk menggerakkan peluru
    public float damage = 10f;  // Damage yang diberikan oleh peluru

    void Start()
    {
        // Mendapatkan Rigidbody2D peluru
        rb = GetComponent<Rigidbody2D>();

        // Menggerakkan peluru sesuai arah tembakan
        rb.velocity = transform.up * bulletSpeed;

        // Menghancurkan peluru setelah 6 detik
        Destroy(gameObject, 15f);  // Hancurkan peluru setelah 6 detik
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek apakah peluru mengenai objek dengan tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Mengurangi health musuh
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // Mengurangi kesehatan musuh
            }
        }
    }
}