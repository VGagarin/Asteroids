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

        private Game _game;
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
            _game.Initialize();
        }

        private void CreateGame()
        {
            _game = new Game();
            
            _game.HealthCountUpdated += OnHealthCountUpdated;
            _game.ScoreCountUpdated += OnScoreCountUpdated;
            _game.MaxScoreCountUpdated += OnMaxScoreCountUpdated;
            _game.GameEnded += OnGameEnded;
        }
        
        private void OnEnemyKilled() => _game.IncreaseScoreCount();
        
        private void OnPlayerDied() => _game.DecreaseHealthCount();

        private void OnHealthCountUpdated(int healthCount) => _hudView.SetHealthCount(healthCount);

        private void OnScoreCountUpdated(int score) => _hudView.SetScore(score);
        
        private void OnMaxScoreCountUpdated(int score) => _hudView.SetRecord(score);

        private void OnGameEnded()
        {
            _enemySpawner.enabled = false;
            _playerSpawner.enabled = false;
            
            _hudView.EnableRestartPanel();
        }

        private void OnDestroy()
        {
            _game.HealthCountUpdated -= OnHealthCountUpdated;
            _game.ScoreCountUpdated -= OnScoreCountUpdated;
            _game.MaxScoreCountUpdated -= OnMaxScoreCountUpdated;
            _game.GameEnded -= OnGameEnded;
            
            _enemySpawner.EnemyKilled -= OnEnemyKilled;
            _playerSpawner.PlayerDied -= OnPlayerDied;
        }
    }
}