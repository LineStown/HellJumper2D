using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SCSIA
{
    public class MainMenuView : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private AudioClip _mainMenuBackgroundMusic;

        //############################################################################################
        // PRIVATE  METHODS
        //############################################################################################
        private void OnEnable()
        {
            SubscribeEvents();
            Bootstrap.AudioManager.PlayMusic(_mainMenuBackgroundMusic, 0.5f, true);
            Bootstrap.AudioManager.PlayMusic(_mainMenuBackgroundMusic, 0.5f);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _newGameButton.onClick.AddListener(OnNewGameButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick); 
        }

        private void UnsubscribeEvents()
        {
            _newGameButton.onClick.RemoveListener(OnNewGameButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void OnNewGameButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnExitButtonClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
