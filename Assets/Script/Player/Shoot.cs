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
    private LineRenderer lineRenderer;  // LineRenderer untuk melacak arah tembakan

    void Start()
    {
        // Pastikan shootPoint di-assign dengan benar
        if (shootPoint == null)
        {
            Debug.LogError("ShootPoint is not assigned in the Inspector!");
        }

        // Menambahkan komponen LineRenderer jika belum ada
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();  // Tambahkan LineRenderer jika belum ada
        }

        lineRenderer.startWidth = 0.1f;  // Lebar garis di awal
        lineRenderer.endWidth = 0.1f;    // Lebar garis di akhir
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // Material standar untuk garis
        lineRenderer.startColor = Color.red;  // Warna garis di awal
        lineRenderer.endColor = Color.red;    // Warna garis di akhir
        lineRenderer.positionCount = 2;  // Jumlah titik garis (start dan end)

        // Menonaktifkan LineRenderer di awal
        lineRenderer.enabled = true;
    }

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

        // Update garis yang menunjukkan arah tembakan jika peluru belum ditembakkan
        if (canShoot)
        {
            UpdateLineRenderer();
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
        // Cegah tembakan lebih dari sekali
        canShoot = false;

        // Buat peluru di posisi shootPoint
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Mendapatkan Rigidbody2D untuk menggerakkan peluru
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Gerakkan peluru ke arah yang sesuai dengan rotasi objek
            rb.velocity = shootPoint.up * bulletSpeed;
        }

        // Menonaktifkan LineRenderer setelah peluru ditembakkan
        lineRenderer.enabled = false;

        // Hancurkan peluru setelah beberapa detik
        Destroy(bullet, 20f);  // Ubah durasi sesuai kebutuhan

        // Setelah peluru dihancurkan, reset status tembakan agar bisa menembak lagi
        Invoke("ResetShoot", 1f);  // Tunggu 1 detik dan aktifkan kembali tembakan
    }

    void UpdateLineRenderer()
    {
        // Cek jika shootPoint dan lineRenderer sudah benar
        if (shootPoint != null && lineRenderer != null)
        {
            // Tentukan posisi awal garis (titik tembak)
            lineRenderer.SetPosition(0, shootPoint.position);

            // Tentukan posisi akhir garis (arah tembakan, panjang garis tergantung dari seberapa jauh garis ingin ditampilkan)
            Vector3 endPosition = shootPoint.position + shootPoint.up * 10f;  // Panjang garis 10 unit (sesuaikan sesuai kebutuhan)
            lineRenderer.SetPosition(1, endPosition);
        }
    }

    // Fungsi untuk mereset tembakan
    // void ResetShoot()
    // {
    //     canShoot = true;  // Mengaktifkan kembali tembakan setelah 1 detik
    //     lineRenderer.enabled = true;  // Mengaktifkan kembali LineRenderer
    // }
}
