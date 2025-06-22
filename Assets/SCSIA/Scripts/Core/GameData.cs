using System;
using System.Collections.Generic;

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
    }
}
