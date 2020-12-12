using System;
using UnityEngine;

namespace Management
{
    internal sealed class GameModel
    {
        private const string MaxScorePrefsKey = "MaxScorePrefsKey";
        private const int HealthCount = 4;
        private const int ScorePerEnemy = 150;

        public event Action<int> HealthCountUpdated;
        public event Action<int> ScoreCountUpdated;
        public event Action<int> MaxScoreCountUpdated;
        public event Action GameEnded; 
        
        private int _currentHealthCount;
        private int _currentScore;
        private int _maxScore;

        public GameModel()
        {
            _currentHealthCount = HealthCount;
            _maxScore = PlayerPrefs.GetInt(MaxScorePrefsKey, 0);
        }

        public void Initialize()
        {
            HealthCountUpdated?.Invoke(_currentHealthCount);
            ScoreCountUpdated?.Invoke(_currentScore);
            MaxScoreCountUpdated?.Invoke(_maxScore);
        }

        public void DecreaseHealthCount()
        {
            if (--_currentHealthCount == 0)
            {
                GameEnded?.Invoke();
            }
            
            HealthCountUpdated?.Invoke(_currentHealthCount);
        }

        public void IncreaseScoreCount()
        {
            _currentScore += ScorePerEnemy;

            if (_currentScore > _maxScore)
            {
                _maxScore = _currentScore;
                
                MaxScoreCountUpdated?.Invoke(_maxScore);
            }
            
            ScoreCountUpdated?.Invoke(_currentScore);
        }
    }
}