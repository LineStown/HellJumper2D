using System;
using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    public class GameDataManager : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        private Action<int> ScoreAction;
        private Action<int> StageAction;
        private Action<int> TimerAction;
        
        private int _score = 0;
        private int _stage = 0;
        private int _timer = 0;
        private int _bestScore = 0;

        private readonly string _recordName = "BestScore";
        private List<string> _log = new List<string>();

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public void SubscribeToScoreAction(Action<int> callback)
        {
            ScoreAction += callback;
        }

        public void SubscribeToStageAction(Action<int> callback)
        {
            StageAction += callback;
        }

        public void SubscribeToTimerAction(Action<int> callback)
        {
            TimerAction += callback;
        }

        public void UnsubscribeToScoreAction(Action<int> callback)
        {
            ScoreAction -= callback;
        }

        public void UnsubscribeToStageAction(Action<int> callback)
        {
            StageAction -= callback;
        }

        public void UnsubscribeToTimerAction(Action<int> callback)
        {
            TimerAction -= callback;
        }

        public void Initialization(int timerValue)
        {
            _score = 0;
            _stage = 0;
            _timer = timerValue;
            _bestScore = (PlayerPrefs.HasKey(_recordName)) ? PlayerPrefs.GetInt(_recordName) : 0;
            _log.Clear();
            AddLog("Round started");
        }

        public void SetScore(int value)
        {
            _score = value;
            ScoreAction?.Invoke(_score);
        }

        public void AddScore(int value)
        {
            _score += _stage * value;
            ScoreAction?.Invoke(_score);
        }

        public int GetScore()
        {
            return _score;
        }

        public int GetBestScore()
        {
            if (PlayerPrefs.HasKey("BestScore"))
                _bestScore = PlayerPrefs.GetInt("BestScore");
            return _bestScore;
        }

        public void SaveScore()
        {
            if (_score > _bestScore)
                PlayerPrefs.SetInt(_recordName, _score);
            PlayerPrefs.Save();
        }

        public void SetStage(int value)
        {
            _stage = value;
            StageAction?.Invoke(_stage);
        }

        public void SetTimer(int value)
        {
            _timer = value;
            TimerAction?.Invoke(_timer);
        }

        public int GetTimer()
        {
            return _timer;
        }

        public void AddLog(string value)
        {
            _log.Add(DateTime.Now + " : " + value);
        }

        public List<string> GetLog()
        { 
            return _log;
        }
    }
}