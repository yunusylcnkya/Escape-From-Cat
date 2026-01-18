// Bu bir "interface" yani bir şablon. 
// Eğer bir nesne bu interface'i kullanırsa, "Boost" adlı bir fonksiyona sahip olmak zorundadır.
public interface IBoostable
{
    // Boost fonksiyonu, oyuncuya bir güçlendirme (boost) vermek için kullanılır.
    // İçine PlayerController gönderilir, böylece hangi oyuncu güçlenecek bilinir.
    void Boost(PlayerController playerController);
}
