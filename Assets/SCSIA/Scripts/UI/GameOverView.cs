using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SCSIA
{
    public class GameOverView : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;

        [Header("Score")]
        [SerializeField] private TextMeshProUGUI _bestScore;
        [SerializeField] private TextMeshProUGUI _yourScore;

        [SerializeField] private GameObject _logSpawnPoint;
        [SerializeField] private GameObject _logPrefab;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        

        //############################################################################################
        // PRIVATE  METHODS
        //############################################################################################
        private void OnEnable()
        {
            SubscribeEvents();
            UpdateScore();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnExitButtonClick()
        {
            SceneManager.LoadScene(0);
        }

        private void UpdateScore()
        {
            _bestScore.text = GameData.GetBestScore().ToString();
            _yourScore.text = GameData.GetScore().ToString();

            GameData.AddLog("Round finished");
            foreach(string logRecord in GameData.GetLog())
            {
                var tmp = Instantiate(_logPrefab, Vector3.zero, Quaternion.identity, _logSpawnPoint.transform);
                tmp.GetComponent<TextMeshProUGUI>().text = logRecord;
            }
        }
    }
}