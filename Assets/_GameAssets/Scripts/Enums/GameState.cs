// GameState bir oyunun hangi durumda olduğunu gösteren bir liste gibi düşün.
// Yani oyun şu anda ne yapıyor veya oyuncu ne yapabilir sorusuna cevap veriyor.
public enum GameState
{
    CutScene,  // Oyun bir hikaye veya video gösteriyor, sen sadece izliyorsun
    Play,      // Oyun başlıyor, sen kontrolü alıyorsun ve oynuyorsun
    Pause,     // Oyun duruyor, her şey beklemede, sen duraklatmışsın
    Resume,    // Oyun tekrar başlıyor, duraklamadan devam ediyor
    GameOver   // Oyun bitti, ya kazandın ya kaybettin
}
