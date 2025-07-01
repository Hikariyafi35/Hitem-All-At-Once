using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Kecepatan rotasi objek
    public float rotationSpeed = 5f;

    void Update()
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
}
