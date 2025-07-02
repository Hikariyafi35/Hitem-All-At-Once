using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Kecepatan peluru
    private Rigidbody2D rb;  // Rigidbody2D peluru untuk menggerakkan peluru
    public float damage = 10f;  // Damage yang diberikan oleh peluru
    private GameManager gameManager;  // Reference ke GameManager   

    void Start()
    {
        // Mendapatkan Rigidbody2D peluru
        rb = GetComponent<Rigidbody2D>();
        // Mendapatkan reference ke GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Menggerakkan peluru sesuai arah tembakan
        rb.velocity = transform.up * bulletSpeed;

        
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
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("OutOfBounds"))
        {
            gameManager.LoseGame();
            Destroy(gameObject);
        }
    }
    public void SplitBullet()
    {
        // Membuat 3 peluru baru yang meluncur ke arah yang berbeda
        CreateBullet(new Vector2(1, 0));  // Peluru pertama ke kanan
        CreateBullet(new Vector2(-1, 0)); // Peluru kedua ke kiri
        CreateBullet(new Vector2(0, 1));  // Peluru ketiga ke atas

        // Hancurkan peluru asli setelah membelah
        Destroy(gameObject);
    }

    // Fungsi untuk membuat peluru baru
    void CreateBullet(Vector2 direction)
    {
        // Membuat peluru baru dari prefab
        GameObject newBullet = Instantiate(gameObject, transform.position, transform.rotation);

        // Menetapkan tag yang berbeda pada peluru clone
        newBullet.tag = "CloneBullet";  // Berikan tag yang berbeda agar tidak terhitung dalam perhitungan GameManager

        // Mendapatkan Rigidbody2D peluru baru dan mengaturnya ke arah yang berbeda
        Rigidbody2D newRb = newBullet.GetComponent<Rigidbody2D>();
        if (newRb != null)
        {
            newRb.velocity = direction * bulletSpeed;  // Menentukan kecepatan dan arah peluru baru
        }
    }
}