using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType
    {
        Clone,  // Buff untuk membagi peluru menjadi beberapa
        Speed,  // Buff untuk meningkatkan kecepatan peluru
        Damage,  // Buff untuk meningkatkan damage peluru
        Size
    }

    public BuffType buffType;  // Tipe buff yang akan diterapkan

    void OnTriggerEnter2D(Collider2D other)
    {
        // Mengecek apakah yang mengenai buff adalah peluru
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                ApplyBuff(bullet);  // Terapkan efek buff pada peluru
                Destroy(gameObject);  // Hancurkan buff setelah terkena peluru
            }
        }
    }

    void ApplyBuff(Bullet bullet)
    {
        switch (buffType)
        {
            case BuffType.Clone:
                bullet.SplitBullet();  // Jika buff adalah Clone, peluru akan membelah
                break;
            case BuffType.Speed:
                bullet.bulletSpeed *= 1.5f;  // Meningkatkan kecepatan peluru
                break;
            case BuffType.Damage:
                bullet.damage *= 2;  // Meningkatkan damage peluru
                break;
            case BuffType.Size:
                bullet.IncreaseSize(10f);  // Memperbesar ukuran peluru
                break;
        }
    }
}
