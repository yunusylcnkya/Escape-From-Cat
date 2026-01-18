using System;
using UnityEngine;

// Bu sınıf, oyundaki kedimizin animasyonlarını kontrol ediyor.
// Yani kedi yürüyorsa yürüyüş animasyonu, koşuyorsa koşu animasyonu çalışıyor.
public class CatAnimationController : MonoBehaviour
{
    // Kedinin animatörünü tutuyoruz (animasyonları çalıştıran şey)
    [SerializeField] private Animator _catAnimator;

    // Kedinin mevcut durumunu öğrenmemizi sağlayan başka bir sınıf
    private CatStateController _catStateController;

    void Awake()
    {
        // Kedinin durumunu takip eden sınıfı alıyoruz
        _catStateController = GetComponent<CatStateController>();
    }

    void Update()
    {
        // Oyun oynanıyor değilse veya başka bir şey yapıyorsa animasyonları kapat
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play)
            && (GameManager.Instance.GetCurrentGameState() != GameState.Resume)
            && (GameManager.Instance.GetCurrentGameState() != GameState.CutScene)
            && (GameManager.Instance.GetCurrentGameState() != GameState.GameOver))
        {
            _catAnimator.enabled = false;
            return;
        }

        // Kedinin durumuna göre doğru animasyonu çalıştır
        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        _catAnimator.enabled = true;

        // Kedinin şu anki durumunu öğreniyoruz
        var currentState = _catStateController.GetCurrentCatState();

        switch (currentState)
        {
            case CatState.Idle:
                // Kedi duruyorsa sadece durma animasyonu açık olsun
                _catAnimator.SetBool(Consts.catAnimations.IS_IDLING, true);
                _catAnimator.SetBool(Consts.catAnimations.IS_WALKING, false);
                _catAnimator.SetBool(Consts.catAnimations.IS_RUNNING, false);
                break;

            case CatState.Walking:
                // Kedi yürüyorsa yürüyüş animasyonu açık olsun
                _catAnimator.SetBool(Consts.catAnimations.IS_IDLING, false);
                _catAnimator.SetBool(Consts.catAnimations.IS_WALKING, true);
                _catAnimator.SetBool(Consts.catAnimations.IS_RUNNING, false);
                break;

            case CatState.Running:
                // Kedi koşuyorsa koşu animasyonu açık olsun
                _catAnimator.SetBool(Consts.catAnimations.IS_RUNNING, true);
                break;

            case CatState.Attacking:
                // Kedi saldırıyorsa saldırı animasyonu açık olsun
                _catAnimator.SetBool(Consts.catAnimations.IS_ATTACKING, true);
                break;
        }
    }
}
