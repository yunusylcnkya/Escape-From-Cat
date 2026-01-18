using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu sınıf, oyundaki bir ses parçasını (efekt veya müzik) tanımlamak için kullanılır
[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource Source; // Sesin çalınacağı gerçek bileşen. Oyuncu bunu görmez, Unity otomatik kullanır.

    public AudioClip AudioClip; // Çalmak istediğimiz ses dosyası (mp3, wav vb.)
    public SoundType SoundType; // Sesi tanımlamak için bir isim. Örneğin "Jump", "Explosion", "BackgroundMusic"

    [Range(0f, 1f)]
    public float Volume; // Sesin ne kadar yüksek olacağını belirler (0=hiç, 1=tam)

    [Range(.1f, 3f)]
    public float Pitch; // Sesin tizliğini veya alçaklığını değiştirir. 1 normal, 2 daha tiz, 0.5 daha alçak gibi.

    public bool Mute; // Eğer true ise ses tamamen sessiz olur
    public bool Loop; // Eğer true ise ses sürekli tekrarlar (müzik gibi)
    public bool playOnAwake; // Eğer true ise oyun başladığında otomatik çalar
}
