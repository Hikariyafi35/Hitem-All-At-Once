using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum BuffType
    {
        Clone,  
        Speed,  
        Damage,  
        Size,
        Magnet
    }

    public BuffType buffType;  

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                ApplyBuff(bullet);  
                Destroy(gameObject);  
            }
        }
    }

    void ApplyBuff(Bullet bullet)
    {
        switch (buffType)
        {
            case BuffType.Clone:
                bullet.SplitBullet();  
                break;
            case BuffType.Speed:
                bullet.bulletSpeed *= 1.5f;  
                break;
            case BuffType.Damage:
                bullet.damage *= 2;  
                break;
            case BuffType.Size:
                bullet.IncreaseSize(7f);  
                break;
            case BuffType.Magnet:
                bullet.EnableMagnet(true, 5f);
                break;
        }
    }
}
