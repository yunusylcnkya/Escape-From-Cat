using UnityEngine;

// PlayerState, oyuncunun ne yaptığını anlatan bir liste gibi düşünebilirsin.
// Yani oyuncu duruyor mu, hareket ediyor mu, zıplıyor mu veya kayıyor mu diye bilgisayara söylüyor.
public enum PlayerState
{
    Idle,       // Oyuncu duruyor, hiçbir şey yapmıyor
    Move,       // Oyuncu yürüyüp koşuyor
    Jump,       // Oyuncu zıplıyor
    SlideIdle,  // Oyuncu kayma pozisyonunda ama henüz hareket etmiyor
    Slide,      // Oyuncu kayıyor
}
