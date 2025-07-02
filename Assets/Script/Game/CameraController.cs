using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 10f;        // Seberapa cepat kamera merespons scroll
    public float smoothTime = 0.2f;        // Seberapa licin pergerakan kamera
    public float minX = -20f;              // Batas kiri
    public float maxX = 100f;              // Batas kanan

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 defaultPosition;

    void Start()
    {
        defaultPosition = new Vector3(0f, 0f, -10f);
        targetPosition = transform.position;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Ubah target posisi berdasarkan scroll input
        if (scroll != 0f)
        {
            targetPosition += Vector3.right * scroll * scrollSpeed;
        }

        // Reset posisi kamera jika middle mouse ditekan
        if (Input.GetMouseButtonDown(2))
        {
            targetPosition = defaultPosition;
        }

        // Batasi posisi X target agar tidak melewati batas
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        // Gerakkan kamera secara halus ke target posisi
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

