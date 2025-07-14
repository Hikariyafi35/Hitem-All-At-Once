using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 10f;        // Seberapa cepat kamera merespons scroll
    public float smoothTime = 0.2f;        // Seberapa licin pergerakan kamera
    public Transform leftBoundary;         // Referensi ke Empty GameObject untuk batas kiri
    public Transform rightBoundary;        // Referensi ke Empty GameObject untuk batas kanan

    private Vector3 velocity = Vector3.zero; // Untuk pergerakan licin
    private Vector3 targetPosition;          // Target posisi kamera
    private Vector3 defaultPosition;         // Posisi default kamera

    void Start()
    {
        //Cursor.visible = false;
        
        // Set posisi default kamera
        defaultPosition = new Vector3(0f, 0f, -10f); // Posisi default pada sumbu X, Y, Z
        targetPosition = transform.position;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");  // Dapatkan input scroll mouse

        // Ubah posisi target berdasarkan scroll input
        if (scroll != 0f)
        {
            targetPosition += Vector3.right * scroll * scrollSpeed;  // Gerakkan kamera hanya di sumbu X
        }

        // Reset posisi kamera jika middle mouse ditekan
        if (Input.GetMouseButtonDown(2))
        {
            targetPosition = defaultPosition; // Kembalikan ke posisi default
        }

        // Batasi posisi target kamera hanya pada sumbu X menggunakan referensi Empty GameObject
        targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary.position.x, rightBoundary.position.x);  // Pembatasan horizontal (X)

        // Gerakkan kamera secara halus menuju target
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
