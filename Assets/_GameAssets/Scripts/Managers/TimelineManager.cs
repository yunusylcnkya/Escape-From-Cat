using System;
using UnityEngine;
using UnityEngine.Playables;

// Bu sınıf, oyunda kesit sahnelerini (cutscenes) oynatıyor.
// Yani oyun başlamadan önce bir mini film gibi sahne gösteriyor.
public class TimelineManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager; // Oyun yöneticisi
    private PlayableDirector _playableDirector;        // Sahneyi oynatan bileşen

    void Awake()
    {
        // Bu objeden sahneyi oynatacak bileşeni al
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        _playableDirector.Play(); // Sahneyi başlat
        _playableDirector.stopped += OnTimeLineFinished; // Sahne bitince ne olacak?
    }

    // Sahne bittiğinde çağrılır
    private void OnTimeLineFinished(PlayableDirector director)
    {
        _gameManager.ChangeGameState(GameState.Play); // Oyunu başlat
    }
}
