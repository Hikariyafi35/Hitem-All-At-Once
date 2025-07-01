using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab peluru
    public Transform shootPoint;     // Titik tembak peluru
    public float rotationSpeed = 5f; // Kecepatan rotasi
    public float bulletSpeed = 10f;  // Kecepatan peluru

    private bool canShoot = true;  // Menandakan apakah tembakan masih diizinkan

    void Update()
    {
        // Mengatur rotasi objek mengikuti mouse
        RotateWithMouse();

        // Tembak hanya jika tombol mouse ditekan dan tembakan masih diizinkan
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            ShootBullet();
            canShoot = false;  // Menonaktifkan tembakan setelah tembakan pertama
        }
    }

    void RotateWithMouse()
    {
        // Mendapatkan posisi mouse di dunia
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Menghitung arah dari objek ke posisi mouse
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0; // Pastikan objek berputar hanya di sumbu 2D

        // Menghitung rotasi objek berdasarkan arah mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Mengatur rotasi objek
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotationSpeed * Time.deltaTime);
    }

    void ShootBullet()
    {
        // Buat peluru di posisi shootPoint
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Mendapatkan Rigidbody2D untuk menggerakkan peluru
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Gerakkan peluru ke arah yang sesuai dengan rotasi objek
            rb.velocity = shootPoint.up * bulletSpeed;
        }

        // Hancurkan peluru setelah beberapa detik (misalnya, 2 detik)
        Destroy(bullet, 20f);  // Ubah durasi sesuai kebutuhan
    }
}
