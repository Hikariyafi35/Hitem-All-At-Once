using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI winText;  // TextMesh Pro untuk teks win
    public GameObject winCanvas;  // Canvas untuk menampilkan win UI
    public TextMeshProUGUI enemyCountText;  // TextMesh Pro untuk menampilkan jumlah musuh
    public TextMeshProUGUI loseText;  // TextMesh Pro untuk teks lose
    public GameObject loseCanvas;  // Canvas untuk menampilkan lose UI

    private int totalEnemies;  // Jumlah total musuh yang ada
    private int currentEnemies;  // Jumlah musuh yang masih hidup

    void Start()
    {
        // Mencari semua musuh di scene dan menghitung total musuh dengan tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;
        currentEnemies = totalEnemies;

        // Pastikan canvas win tidak terlihat di awal
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);

        // Menampilkan jumlah total musuh di UI
        UpdateEnemyCountText();
    }

    // Fungsi ini akan dipanggil ketika musuh mati
    public void EnemyDied()
    {
        currentEnemies--;  // Kurangi jumlah musuh yang hidup

        // Menampilkan jumlah musuh yang tersisa di Console (untuk debugging)
        Debug.Log("Enemies left: " + currentEnemies);

        // Update jumlah musuh yang tersisa di UI
        UpdateEnemyCountText();

        // Jika semua musuh sudah mati
        if (currentEnemies <= 0)
        {
            WinGame();  // Menampilkan UI Win
        }
    }

    // Fungsi untuk menampilkan UI Win
    void WinGame()
    {
        winCanvas.SetActive(true);  // Menampilkan Canvas Win
        winText.text = "You Win!";  // Menampilkan teks "You Win"

        // Menghancurkan semua peluru yang ada di game
        DestroyAllBullets();
    }

    // Fungsi untuk menghancurkan semua peluru yang ada di game
    void DestroyAllBullets()
    {
        // Mencari semua objek peluru yang ada di scene dengan tag "Bullet" atau "CloneBullet"
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");  // Peluru asli
        GameObject[] cloneBullets = GameObject.FindGameObjectsWithTag("CloneBullet");  // Peluru clone

        // Menghancurkan setiap peluru
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);  // Menghancurkan peluru asli
        }

        foreach (GameObject cloneBullet in cloneBullets)
        {
            Destroy(cloneBullet);  // Menghancurkan peluru clone
        }
    }

    // Fungsi untuk menampilkan UI Lose
    public void LoseGame()
    {
        loseCanvas.SetActive(true);  // Menampilkan Canvas Lose
        loseText.text = "You Lose!";  // Menampilkan teks "You Lose"

        // Menghancurkan semua peluru yang ada di game
        DestroyAllBullets();
    }

    // Fungsi untuk memperbarui teks jumlah musuh yang tersisa
    void UpdateEnemyCountText()
    {
        enemyCountText.text = "" + currentEnemies;
    }
}