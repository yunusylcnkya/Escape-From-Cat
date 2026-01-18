using UnityEngine;
using UnityEngine.EventSystems;

// Bu sınıf, bir butonun üzerine fare geldiğinde çalışıyor.
// Yani fareyi butonun üstüne getirince bir ses çıkıyor.
public class ButtonHoverController : MonoBehaviour, IPointerEnterHandler
{
    // Fare butonun üstüne gelince bu fonksiyon çalışır
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ses oynat: fare butonun üstüne geldi
        AudioManager.Instance.Play(SoundType.ButtonHoverSound);
    }
}
