using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SCSIA
{
    public class GameData
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        private static Action<int> ScoreAction;
        private static Action<int> StageAction;
        private static Action<int> TimerAction;
        
        private static int _score = 0;
        private static int _stage = 0;
        private static int _timer = 0;
        private static int _bestScore = 0;

        private static readonly string _recordName = "BestScore";
        private static List<string> _log = new List<string>();

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public static void SubscribeToScoreAction(Action<int> callback)
        {
            ScoreAction += callback;
        }

        public static void SubscribeToStageAction(Action<int> callback)
        {
            StageAction += callback;
        }

        public static void SubscribeToTimerAction(Action<int> callback)
        {
            TimerAction += callback;
        }

        public static void UnsubscribeToScoreAction(Action<int> callback)
        {
            ScoreAction -= callback;
        }

        public static void UnsubscribeToStageAction(Action<int> callback)
        {
            StageAction -= callback;
        }

        public static void UnsubscribeToTimerAction(Action<int> callback)
        {
            TimerAction -= callback;
        }

        public static void Initialization(int timerValue)
        {
            _score = 0;
            _stage = 0;
            _timer = timerValue;
            _bestScore = (PlayerPrefs.HasKey(_recordName)) ? PlayerPrefs.GetInt(_recordName) : 0;
            _log.Clear();
            AddLog("Round started");
        }

        public static void SetScore(int value)
        {
            _score = value;
            ScoreAction?.Invoke(_score);
        }

        public static void AddScore(int value)
        {
            _score += _stage * value;
            ScoreAction?.Invoke(_score);
        }

        public static int GetScore()
        {
            return _score;
        }

        public static int GetBestScore()
        {
            if (PlayerPrefs.HasKey("BestScore"))
                _bestScore = PlayerPrefs.GetInt("BestScore");
            return _bestScore;
        }

        public static void SaveScore()
        {
            if (_score > _bestScore)
                PlayerPrefs.SetInt(_recordName, _score);
            PlayerPrefs.Save();
        }

        public static void SetStage(int value)
        {
            _stage = value;
            StageAction?.Invoke(_stage);
        }

        public static void SetTimer(int value)
        {
            _timer = value;
            TimerAction?.Invoke(_timer);
        }

        public static int GetTimer()
        {
            return _timer;
        }

        public static void AddLog(string value)
        {
            _log.Add(DateTime.Now + " : " + value);
        }

        public static List<string> GetLog()
        { 
            return _log;
        }
    }
}