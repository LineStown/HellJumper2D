using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    public class ViewManager : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Canvas _gameViewCanvas;
        [SerializeField] private Canvas _gamePauseViewCanvas;
        [SerializeField] private Canvas _gameOverViewCanvas;
        [SerializeField] private List<GameObject> _stoppableObjects;
        [SerializeField] private AudioSource _audioSource;
        private Canvas _currentView;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public void CallGamePauseView()
        {
            if(_currentView == _gameViewCanvas)
            {
                _currentView = _gamePauseViewCanvas;
                foreach(GameObject obj in _stoppableObjects)
                    obj.SetActive(false);
                _gamePauseViewCanvas.gameObject.SetActive(true);
                _gameViewCanvas.gameObject.SetActive(false);
                _audioSource.Pause();
            }
        }

        public void CallGameOverView()
        {
            if (_currentView == _gameViewCanvas)
            {
                _currentView = _gameOverViewCanvas;
                foreach (GameObject obj in _stoppableObjects)
                    obj.SetActive(false);
                _gameOverViewCanvas.gameObject.SetActive(true);
                _gameViewCanvas.gameObject.SetActive(false); 
            }
        }

        public void CallGameView()
        {
            if (_currentView == _gamePauseViewCanvas)
            {
                _currentView = _gameViewCanvas;
                foreach (GameObject obj in _stoppableObjects)
                    obj.SetActive(true);
                _gameViewCanvas.gameObject.SetActive(true);
                _gamePauseViewCanvas.gameObject.SetActive(false);
                _audioSource.UnPause();
            }
        }

        //############################################################################################
        // PRIVATE  METHODS
        //############################################################################################
        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            _currentView = _gameViewCanvas;
        }
    }
}
