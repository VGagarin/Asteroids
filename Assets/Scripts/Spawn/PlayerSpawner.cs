using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    internal sealed class PlayerSpawner : MonoBehaviour
    {
        private const float HorizontalAmplitude = 7f;
        private const float VerticalAmplitude = 4f;
        private const float SpawnDelay = 3f;

        public event Action PlayerDied;
        
        [SerializeField] private Player.Player _playerPrefab;
        [SerializeField] private Transform _centerOfSpawn;

        private Player.Player _player;
        private bool _isDied;
        private float _spawnDelay;

        private void Start()
        {
            Spawn();
        }

        private void FixedUpdate()
        {
            _spawnDelay -= Time.fixedDeltaTime;
            
            if (_isDied && _spawnDelay < 0)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _isDied = false;
            
            Vector3 center = _centerOfSpawn.position;
            
            Vector2 spawnPosition = new Vector2(
                center.x + Random.Range(-HorizontalAmplitude, HorizontalAmplitude),
                center.y + Random.Range(-VerticalAmplitude, VerticalAmplitude));

            _player = Instantiate(_playerPrefab, spawnPosition, Quaternion.identity);
            
            _player.transform.Rotate(Vector3.forward, Random.Range(0f, 2f * Mathf.PI * Mathf.Rad2Deg));
            _player.Died += OnPlayerDie;
        }

        private void OnPlayerDie()
        {
            _player.Died -= OnPlayerDie;
            
            _isDied = true;
            _spawnDelay = SpawnDelay;
            
            PlayerDied?.Invoke();
        }
    }
}