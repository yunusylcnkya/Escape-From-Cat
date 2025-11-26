using MaskTransitions;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TimerUI _timerUI;

    void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
        _mainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);

        });
    }

    private void OnOneMoreButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}
