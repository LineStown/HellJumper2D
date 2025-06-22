using System;
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
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameData.SubscribeToScoreAction(OnScoreAction);
            GameData.SubscribeToStageAction(OnStageAction);
            GameData.SubscribeToTimerAction(OnTimerAction);
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void UnsubscribeEvents()
        {
            GameData.UnsubscribeToScoreAction(OnScoreAction);
            GameData.UnsubscribeToStageAction(OnStageAction);
            GameData.UnsubscribeToTimerAction(OnTimerAction);
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
