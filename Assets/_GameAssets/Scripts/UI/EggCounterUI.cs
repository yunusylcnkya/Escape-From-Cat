using DG.Tweening;
using TMPro;
using UnityEngine;

// Bu sınıf, ekrandaki "yumurta sayısı" göstergesini kontrol ediyor.
public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText; // Yumurtaları gösteren yazı

    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;  // Yumurtalar tamamlandığında renk
    [SerializeField] private float _colorDuration;    // Renk değişim süresi
    [SerializeField] private float _scaleDuration;    // Yazının büyüme süresi

    private RectTransform _eggCounterRectTransform; // Yazının boyutunu ve konumunu kontrol etmek için

    void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }

    // Yumurtaları güncelle
    public void SetEggCounterText(int counter, int max)
    {
        _eggCounterText.text = counter.ToString() + "/" + max.ToString();
        // Örnek: "3/5" gibi ekranda görünür
    }

    // Tüm yumurtalar toplandığında
    public void SetEggCompleted()
    {
        // Yazının rengini değiştir
        _eggCounterText.DOColor(_eggCounterColor, _colorDuration);

        // Yazıyı biraz büyüt ve geri küçült (güzel animasyon efekti)
        _eggCounterRectTransform.DOScale(1.2f, _scaleDuration).SetEase(Ease.OutBack);
    }
}
