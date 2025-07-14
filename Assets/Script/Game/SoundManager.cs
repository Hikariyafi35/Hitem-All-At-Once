using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;  // Singleton instance untuk akses dari script lain
    public AudioSource musicSource;       // AudioSource untuk musik latar
    public AudioSource effectsSource;     // AudioSource untuk efek suara

    public List<AudioClip> musicClips = new List<AudioClip>();        // Daftar clip musik yang bisa diputar
    public List<AudioClip> soundEffects = new List<AudioClip>();      // Daftar efek suara yang bisa diputar

    public float musicVolume = 0.5f;      // Volume musik
    public float effectsVolume = 0.5f;    // Volume efek suara

    void Awake()
    {
        // Menjaga agar hanya ada satu instance dari SoundManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Menjaga agar SoundManager tidak dihancurkan ketika scene berubah
        DontDestroyOnLoad(gameObject);

        // Menambahkan listener untuk scene yang dimuat
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void Start()
    {
        // Menyetel volume audio
        musicSource.volume = musicVolume;
        effectsSource.volume = effectsVolume;

        // Memutar musik sesuai dengan scene yang pertama kali dimuat
        PlayMusicBasedOnScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi untuk memulai musik berdasarkan nama scene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene(scene.name);
    }

    // Fungsi untuk memutar musik berdasarkan nama scene
    void PlayMusicBasedOnScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Main Menu":
                PlayMusic("Main Theme");  // Main Menu menggunakan Main Theme
                break;
            case "Level 1":
                PlayMusic("lvl1");  // Level 1 menggunakan musik lvl 1
                break;
            case "Level 2":
                PlayMusic("lvl2");  // Level 2 menggunakan musik lvl 2
                break;
            case "Level 3":
                PlayMusic("lvl1");  // Level 3 menggunakan musik lvl 1
                break;
            case "Level 4":
                PlayMusic("lvl2");  // Level 4 menggunakan musik lvl 2
                break;
            default:
                PlayMusic("Main Theme");  // Default ke Main Theme
                break;
        }
    }

    // Fungsi untuk memulai musik
    public void PlayMusic(string musicName)
    {
        // Mencari clip musik berdasarkan nama
        AudioClip musicClip = musicClips.Find(clip => clip.name == musicName);
        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music not found: " + musicName);
        }
    }

    // Fungsi untuk memutar efek suara
    public void PlaySoundEffect(string soundName)
    {
        // Mencari efek suara berdasarkan nama
        AudioClip soundClip = soundEffects.Find(clip => clip.name == soundName);
        if (soundClip != null)
        {
            effectsSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    // Fungsi untuk mengatur volume musik
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    // Fungsi untuk mengatur volume efek suara
    public void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
        effectsSource.volume = effectsVolume;
    }
}