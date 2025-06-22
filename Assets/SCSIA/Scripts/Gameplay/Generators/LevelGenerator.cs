using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SCSIA
{
    public class LevelGenerator : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Level generator config")]
        [SerializeField] private LevelGeneratorConfig _levelGeneratorConfig;
        [SerializeField] private ViewManager _viewManager;

        private int _levelTimer = -1;
        private Coroutine _levelTimerCoroutineId;

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Awake()
        {
            Initialization();
        }

        private void OnEnable()
        {
            StartLevel();
        }

        private void OnDisable()
        {
            StopLevel();
        }

        private void StartLevel()
        {
            if (_levelTimer == -1)
                _levelTimer = _levelGeneratorConfig.LevelTimer;
            _levelTimerCoroutineId = StartCoroutine(LevelTimer());
        }

        private void Initialization()
        {
            GameData.Initialization(_levelGeneratorConfig.LevelTimer);
        }

        private void StopLevel()
        {
            StopCoroutine(_levelTimerCoroutineId);
        }

        private IEnumerator LevelTimer()
        {
            do
            {
                GameData.SetTimer(_levelTimer);
                yield return new WaitForSeconds(1f);
                _levelTimer--;
            }
            while (_levelTimer >= 0);
            FinishLevel();
        }

        private void FinishLevel()
        {
            _viewManager.CallGameOverView();
        }
    }
}
