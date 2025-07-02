using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;  // Kecepatan peluru
    private Rigidbody2D rb;  // Rigidbody2D peluru untuk menggerakkan peluru
    public float damage = 10f;  // Damage yang diberikan oleh peluru
    private GameManager gameManager;  // Reference ke GameManager   
    private bool isActive = true;

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
        if (!isActive) return; // Tidak aktif? Jangan deteksi tabrakan

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
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
        GameObject newBullet = Instantiate(gameObject, transform.position, transform.rotation);

        newBullet.tag = "CloneBullet";

        Rigidbody2D newRb = newBullet.GetComponent<Rigidbody2D>();
        if (newRb != null)
        {
            newRb.velocity = direction * bulletSpeed;
        }

        // Nonaktifkan collider sesaat untuk menghindari trigger overlap
        newBullet.GetComponent<Bullet>().StartCoroutine(newBullet.GetComponent<Bullet>().DelayedActivate());
    }
    public IEnumerator DelayedActivate()
    {
        isActive = false;
        yield return new WaitForSeconds(0.05f); // Delay 50ms
        isActive = true;
    }
    public void IncreaseSize(float multiplier)
    {
        transform.localScale *= multiplier;
    }
    public void IncreaseSizeTemporary(float multiplier, float duration)
    {
        StartCoroutine(SizeBuffRoutine(multiplier, duration));
    }

    private IEnumerator SizeBuffRoutine(float multiplier, float duration)
    {
        transform.localScale *= multiplier;
        yield return new WaitForSeconds(duration);
        transform.localScale /= multiplier;  // Kembali ke ukuran semula
    }
}