using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SCSIA
{
    public class GamePauseView : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;

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
            _backButton.onClick.AddListener(OnbackButtonClick);
            _newGameButton.onClick.AddListener(OnNewGameButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void UnsubscribeEvents()
        {
            _backButton.onClick.RemoveListener(OnbackButtonClick);
            _newGameButton.onClick.RemoveListener(OnNewGameButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnbackButtonClick()
        {
            _viewManager.CallGameView();
        }

        private void OnNewGameButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnExitButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}