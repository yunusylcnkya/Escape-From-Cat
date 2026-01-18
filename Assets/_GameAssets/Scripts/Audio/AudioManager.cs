using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Tek bir AudioManager olmasını sağlamak için "Instance" adında bir referans
    public static AudioManager Instance { get; private set; }

    [Header("Sounds")]
    // Oyunda kullanılacak tüm sesler burada tutuluyor
    public Sound[] Sounds;

    private void Awake()
    {
        // Bu sınıfın tek bir örneği olduğunu ayarla
        Instance = this;

        // Her ses için bir AudioSource ekle ve ayarlarını yap
        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>(); // Ses çalmak için bileşen ekle
            s.Source.clip = s.AudioClip;                        // Ses dosyasını ata
            s.Source.volume = s.Volume;                         // Ses yüksekliğini ayarla
            s.Source.pitch = s.Pitch;                           // Sesin hızını/pitch'ini ayarla
            s.Source.mute = s.Mute;                             // Sessize alıp almamayı ayarla
            s.Source.loop = s.Loop;                             // Tekrar etsin mi, etmesin mi
            s.Source.playOnAwake = s.playOnAwake;               // Oyun başlar başlamaz çalsın mı
        }
    }

    // Belirli bir türdeki sesi çal
    public void Play(SoundType soundType)
    {
        // Sesleri ara ve türüne göre bul
        Sound s = Array.Find(Sounds, sound => sound.SoundType == soundType);
        if (s == null)
        {
            Debug.LogWarning($"Sound with type {soundType} not found in AudioManager.");
            return; // Eğer ses yoksa çık
        }

        s.Source.Play(); // Bulunan sesi çal
    }

    // Belirli bir türdeki sesi durdur
    public void Stop(SoundType soundType)
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundType == soundType);
        if (s == null)
        {
            Debug.LogWarning($"Sound with type {soundType} not found in AudioManager.");
            return; // Eğer ses yoksa çık
        }

        s.Source.Stop(); // Bulunan sesi durdur
    }

    // Tüm ses efektlerini sessize al veya aç
    public void SetSoundEffectsMute(bool isMuted)
    {
        foreach (Sound s in Sounds)
        {
            s.Source.mute = isMuted; // Sessize al veya aç
        }
    }
}
