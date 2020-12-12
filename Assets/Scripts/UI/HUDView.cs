using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    internal sealed class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _recordText;
        [SerializeField] private GameObject _healthPrefab;
        [SerializeField] private Transform _healthBar;
        [SerializeField] private GameObject _restartPanel;

        private Stack<GameObject> _lifeIndicators = new Stack<GameObject>();

        public void EnableRestartPanel() => _restartPanel.SetActive(true);
        
        public void SetScore(int score) => _scoreText.text = $"{score}";

        public void SetRecord(int score) => _recordText.text = $"{score}";
        
        public void SetHealthCount(int desireCount)
        {
            int current = _lifeIndicators.Count;

            if (current < desireCount)
            {
                AddHealthCount(desireCount - current);
            }
            else
            {
                RemoveHealthCount(current - desireCount);
            }
        }

        private void AddHealthCount(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject lifeIndicator = Instantiate(_healthPrefab, _healthBar);

                _lifeIndicators.Push(lifeIndicator);
            }
        }

        private void RemoveHealthCount(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject lifeIndicator = _lifeIndicators.Pop();

                Destroy(lifeIndicator);
            }
        }
    }
}