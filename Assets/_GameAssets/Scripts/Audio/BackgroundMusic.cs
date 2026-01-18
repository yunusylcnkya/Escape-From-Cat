using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Oyunda sadece bir tane arka plan müziği olmasını sağlamak için "Instance"
    public static BackgroundMusic Instance { get; private set; }

    private AudioSource _audioSource; // Müziği çalmak için AudioSource bileşeni

    private void Awake()
    {
        // Bu nesnenin AudioSource bileşenini al
        _audioSource = GetComponent<AudioSource>();

        // Eğer başka bir BackgroundMusic zaten varsa bu nesneyi yok et
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // Eğer yoksa, bunu tek örnek olarak ayarla
            Instance = this;
            // Bu nesneyi sahneler değişse bile yok etme
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Müzikleri sessize al veya aç
    public void SetMusicMute(bool isMuted)
    {
        _audioSource.mute = isMuted; // true ise sessiz, false ise aç
    }

    // Arka plan müziğini başlat veya durdur
    public void PlayBackgroundMusic(bool isMusicPlaying)
    {
        if (isMusicPlaying && !_audioSource.isPlaying)
            _audioSource.Play(); // Müziği çal
        else if (!isMusicPlaying)
            _audioSource.Stop(); // Müziği durdur
    }
}
