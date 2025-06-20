using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SCSIA
{
    public class MainMenu : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Button _newGame;
        [SerializeField] private Button _leaderboard;
        [SerializeField] private Button _exit;

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
            _newGame.onClick.AddListener(OnNewGameClick);
            _leaderboard.onClick.AddListener(OnLeaderboardClick);
            _exit.onClick.AddListener(OnExitClick);
        }

        private void UnsubscribeEvents()
        {
            _newGame.onClick.RemoveListener(OnNewGameClick);
        }

        private void OnNewGameClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnLeaderboardClick()
        {

        }

        private void OnExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
