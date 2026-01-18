using UnityEngine;

// Bu sınıf, oyundaki "buğday tasarımları" için bir veri kutusu gibi çalışıyor.
// ScriptableObject sayesinde bu bilgileri oyun sahnesine bağlamadan saklayabiliyoruz.
[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObject/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier; // Buğday toplarken veya kaybederken hızı artıran sayı
    [SerializeField] private float _resetBoosDuration; // Hız artışı ne kadar sürecek

    [SerializeField] private Sprite _activeSprite; // Buğday aktifken görünen resim
    [SerializeField] private Sprite _passiveSprite; // Buğday pasifken görünen resim

    [SerializeField] private Sprite _activeWheatSprite; // Altın veya özel buğday aktif resmi
    [SerializeField] private Sprite _passiveWheatSprite; // Altın veya özel buğday pasif resmi

    // Bu değerleri diğer kodların okuyabilmesi için public olarak veriyoruz
    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoosDuration;

    public Sprite ActiveSprite => _activeSprite;
    public Sprite PassiveSprite => _passiveSprite;
    public Sprite ActiveWheatSprite => _activeWheatSprite;
    public Sprite PassiveWheatSprite => _passiveWheatSprite;
}
