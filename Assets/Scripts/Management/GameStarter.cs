using Spawn;
using UI;
using UnityEngine;

namespace Management
{
    internal sealed class GameStarter : MonoBehaviour
    {
        [SerializeField] private HUDView _hudViewPrefab;
        [SerializeField] private PlayerSpawner _playerSpawnerPrefab;
        [SerializeField] private EnemySpawner _enemySpawnerPrefab;
        [SerializeField] private Teleport _teleportPrefab;

        private GameModel _gameModel;
        private HUDView _hudView;
        private EnemySpawner _enemySpawner;
        private PlayerSpawner _playerSpawner;
        
        private void Awake()
        {
            Instantiate(_teleportPrefab);
            
            _hudView = Instantiate(_hudViewPrefab);

            _enemySpawner = Instantiate(_enemySpawnerPrefab);
            _enemySpawner.EnemyKilled += OnEnemyKilled;
            
            _playerSpawner = Instantiate(_playerSpawnerPrefab);
            _playerSpawner.PlayerDied += OnPlayerDied;

            CreateGame();
        }

        private void Start()
        {
            _gameModel.Initialize();
        }

        private void CreateGame()
        {
            _gameModel = new GameModel();
            
            _gameModel.HealthCountUpdated += OnHealthCountUpdated;
            _gameModel.ScoreCountUpdated += OnScoreCountUpdated;
            _gameModel.MaxScoreCountUpdated += OnMaxScoreCountUpdated;
            _gameModel.GameEnded += OnGameModelEnded;
        }
        
        private void OnEnemyKilled() => _gameModel.IncreaseScoreCount();
        
        private void OnPlayerDied() => _gameModel.DecreaseHealthCount();

        private void OnHealthCountUpdated(int healthCount) => _hudView.SetHealthCount(healthCount);

        private void OnScoreCountUpdated(int score) => _hudView.SetScore(score);
        
        private void OnMaxScoreCountUpdated(int score) => _hudView.SetRecord(score);

        private void OnGameModelEnded()
        {
            _enemySpawner.enabled = false;
            _playerSpawner.enabled = false;
            
            _hudView.EnableRestartPanel();
        }

        private void OnDestroy()
        {
            _gameModel.HealthCountUpdated -= OnHealthCountUpdated;
            _gameModel.ScoreCountUpdated -= OnScoreCountUpdated;
            _gameModel.MaxScoreCountUpdated -= OnMaxScoreCountUpdated;
            _gameModel.GameEnded -= OnGameModelEnded;
            
            _enemySpawner.EnemyKilled -= OnEnemyKilled;
            _playerSpawner.PlayerDied -= OnPlayerDied;
        }
    }
}