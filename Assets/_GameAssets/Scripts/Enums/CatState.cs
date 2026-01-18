using UnityEngine;

// CatState bir "kedi durumu listesi" gibi düşünebilirsin.
// Yani kedimiz ne yapıyor sorusuna cevap veriyor: oturuyor mu, yürüyor mu, koşuyor mu yoksa saldırıyor mu?
public enum CatState
{
    Idle,      // Kedimiz sadece duruyor, hiçbir şey yapmıyor
    Walking,   // Kedimiz yavaş yavaş yürüyor
    Running,   // Kedimiz hızlıca koşuyor
    Attacking  // Kedimiz bir şeye saldırıyor
}
