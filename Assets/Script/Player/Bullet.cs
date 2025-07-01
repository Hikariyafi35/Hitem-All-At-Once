using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Kecepatan peluru
    private Rigidbody2D rb;  // Rigidbody2D peluru untuk menggerakkan peluru
    private bool hasBounced = false;  // Untuk mengecek apakah peluru sudah memantul atau belum

    void Start()
    {
        // Mendapatkan Rigidbody2D peluru
        rb = GetComponent<Rigidbody2D>();

        // Menggerakkan peluru sesuai arah tembakan
        rb.velocity = transform.up * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek apakah peluru mengenai objek dengan tag "Wall"
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Mengatur peluru untuk memantul dengan memodifikasi arah kecepatannya
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectDirection * bulletSpeed;  // Memantulkan peluru sesuai dengan kecepatan awal

            // Menandai bahwa peluru sudah memantul
            hasBounced = true;
        }

        // Jika peluru sudah memantul, lakukan tindakan lebih lanjut (misalnya menghancurkan peluru setelah beberapa detik)
        if (hasBounced)
        {
            Destroy(gameObject, 2f);  // Menghancurkan peluru setelah beberapa detik setelah memantul
        }
    }
}
