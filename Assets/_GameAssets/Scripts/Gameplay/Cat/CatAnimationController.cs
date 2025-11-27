using System;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _catAnimator;

    private CatStateController _catStateController;

    void Awake()
    {
        _catStateController = GetComponent<CatStateController>();

    }

    void Update()
    {
        if ((GameManager.Instance.GetCurrentGameState() != GameState.Play)
        && (GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        && (GameManager.Instance.GetCurrentGameState() != GameState.CutScene)
        && (GameManager.Instance.GetCurrentGameState() != GameState.GameOver))
        {
            _catAnimator.enabled = false;
            return;
        }

        SetCatAnimations();
    }

    private void SetCatAnimations()
    {
        _catAnimator.enabled = true;
        var currentState = _catStateController.GetCurrentCatState();
        switch (currentState)
        {
            case CatState.Idle:
                _catAnimator.SetBool(Consts.catAnimations.IS_IDLING, true);
                _catAnimator.SetBool(Consts.catAnimations.IS_WALKING, false);
                _catAnimator.SetBool(Consts.catAnimations.IS_RUNNING, false);
                break;

            case CatState.Walking:
                _catAnimator.SetBool(Consts.catAnimations.IS_IDLING, false);
                _catAnimator.SetBool(Consts.catAnimations.IS_WALKING, true);
                _catAnimator.SetBool(Consts.catAnimations.IS_RUNNING, false);
                break;
            case CatState.Running:
                _catAnimator.SetBool(Consts.catAnimations.IS_RUNNING, true);
                break;

            case CatState.Attacking:
                _catAnimator.SetBool(Consts.catAnimations.IS_ATTACKING, true);

                break;
        }
    }
}
