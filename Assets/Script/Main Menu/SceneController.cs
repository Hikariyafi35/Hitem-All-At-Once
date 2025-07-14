using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Fungsi untuk mengulang scene
    public void RestartScene()
    {
        // Mendapatkan nama scene yang sedang aktif
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Memuat ulang scene yang sama
        SceneManager.LoadScene(currentSceneName);
    }
    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Debug.Log("Keluar dari game..."); // Ini hanya muncul saat di editor
        Application.Quit();
    }
}
