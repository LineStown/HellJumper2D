using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SCSIA
{
    public class GameView : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Button _menuButton;

        //############################################################################################
        // PRIVATE  METHODS
        //############################################################################################
        private void OnEnable()
        {
            UpdateTimerText();
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UpdateTimerText()
        {
            _timerText.text = Bootstrap.GameDataManager.GetTimer().ToString();
        }

        private void SubscribeEvents()
        {
            Bootstrap.GameDataManager?.SubscribeToScoreAction(OnScoreAction);
            Bootstrap.GameDataManager?.SubscribeToStageAction(OnStageAction);
            Bootstrap.GameDataManager?.SubscribeToTimerAction(OnTimerAction);
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void UnsubscribeEvents()
        {
            Bootstrap.GameDataManager?.UnsubscribeToScoreAction(OnScoreAction);
            Bootstrap.GameDataManager?.UnsubscribeToStageAction(OnStageAction);
            Bootstrap.GameDataManager?.UnsubscribeToTimerAction(OnTimerAction);
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void OnScoreAction(int value)
        {
            _scoreText.text = value.ToString();
        }

        private void OnStageAction(int value)
        {
            _stageText.text = value.ToString();
        }

        private void OnTimerAction(int value)
        {
            _timerText.text = value.ToString();
        }

        private void OnMenuButtonClick()
        {
            _viewManager.CallGamePauseView();
        }
    }
}
