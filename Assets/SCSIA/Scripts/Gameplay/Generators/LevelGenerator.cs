using System;
using System.Collections;
using UnityEngine;

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
        private void OnEnable()
        {
            StartLevel();
        }

        private void Start()
        {
            UpdateLevel();
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

        private void UpdateLevel()
        {
            GameData.SetScore(0);
            GameData.SetStage(0);
            GameData.SetTimer(_levelGeneratorConfig.LevelTimer);
        }

        private void StopLevel()
        {
            StopCoroutine(_levelTimerCoroutineId);
        }

        private IEnumerator LevelTimer()
        {
            do
            {
                yield return new WaitForSeconds(1f);
                _levelTimer--;
                GameData.SetTimer(_levelTimer);
            }
            while (_levelTimer > 0);
            yield return new WaitForSeconds(1f);
            FinishLevel();
        }

        private void FinishLevel()
        {
            _viewManager.CallGameOverView();
        }
    }
}
