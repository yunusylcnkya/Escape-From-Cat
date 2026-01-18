// ICollectible: "Toplanabilir" nesneler için bir sözleşme
// Yani bu arayüzü kullanan her nesne, bir Collect() fonksiyonuna sahip olmak zorunda
public interface ICollectible
{
    // Collect() fonksiyonu çağrıldığında
    // oyuncu nesneyi topladığında yapılacakları belirler
    void Collect();
}
