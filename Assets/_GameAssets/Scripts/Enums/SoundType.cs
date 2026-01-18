// SoundType, oyundaki farklı sesleri listeleyen bir "ses kutusu" gibi düşünebilirsin.
// Yani bilgisayar, hangi sesin çalacağını bilmek için bu listeden birini seçiyor.
public enum SoundType
{
    ButtonHoverSound,   // Fareyle butonun üzerine gelince çıkan ses
    ButtonClickSound,   // Butona tıklayınca çıkan ses
    CatSound,           // Kedi sesi
    InteractionSound,   // Oyundaki etkileşim sesleri (bir şey alırken, açarken vs.)
    JumpSound,          // Zıplama sesi
    LoseSound,          // Oyunu kaybedince çıkan ses
    SpatulaSound,       // Spatula ile ilgili bir ses (mesela yemek karıştırırken)
    TransitionSound,    // Sahne değiştirirken çıkan ses
    WinSound,           // Oyunu kazanınca çıkan ses
    PickupGoodSound,    // Oyuncu iyi bir şeyi aldığında çıkan ses
    PickupBadSound,     // Oyuncu kötü bir şeyi aldığında çıkan ses
    ChickSound          // Civciv veya küçük bir kuş sesi
}
