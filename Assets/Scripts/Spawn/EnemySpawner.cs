using System;
using Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    internal sealed class EnemySpawner : MonoBehaviour
    {
        private const float OffsetOfSpawnRadius = 5f;
        private const float MinSpeed = 0.05f;
        private const float MaxSpeed = 0.1f;
        private const float MinDelayBeforeSpawn = 1f;
        private const float MaxDelayBeforeSpawn = 5f;
        private const float ChanceForAlien = 0.05f;
        
        public event Action EnemyKilled;

        [SerializeField] private Transform _circleCenter;
        [SerializeField] private Transform _pointInCircle;
        [SerializeField] private Enemy _asteroidPrefab;
        [SerializeField] private Enemy _alienPrefab;

        private float _radius;
        private float _delayBeforeSpawn;

        private Vector2 RandomPoint
        {
            get
            {
                float radius = RandomSpawnRadius;
                float angle = RandomAngle;

                return new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
            }
        }
        
        private float RandomSpawnRadius => _radius + Random.Range(0, OffsetOfSpawnRadius);
        private float RandomAngle => Random.Range(0, Mathf.PI * 2);
        private bool IsAlianSpawn => Random.Range(0f, 1f) <= ChanceForAlien;

        private void Awake()
        {
            _radius = Vector3.Distance(_circleCenter.position, _pointInCircle.position);
        }

        private void FixedUpdate()
        {
            _delayBeforeSpawn -= Time.fixedDeltaTime;

            if (_delayBeforeSpawn <= 0)
            {
                Spawn();

                _delayBeforeSpawn = Random.Range(MinDelayBeforeSpawn, MaxDelayBeforeSpawn);
            }
        }

        private void Spawn()
        {
            Enemy enemy = Instantiate(IsAlianSpawn ? _alienPrefab : _asteroidPrefab, RandomPoint, Quaternion.identity);

            enemy.SetDestroyAction(OnEnemyKilled);
            SetRandomSpeed(enemy);
        }
        
        private void SetRandomSpeed(Enemy enemy)
        {
            Vector3 centerDirection = _circleCenter.position - enemy.transform.position;
            
            float deflectionAmplitude = Mathf.Asin(_radius / centerDirection.magnitude);
            float deflection = Random.Range(-deflectionAmplitude, deflectionAmplitude);
            
            Movable movableComponent = enemy.GetComponent<Movable>();
            
            movableComponent.Speed = centerDirection.normalized;
            movableComponent.IncreaseSpeedVector(Random.Range(MinSpeed, MaxSpeed));
            movableComponent.RotateSpeedVector(deflection);
        }

        private void OnEnemyKilled()
        {
            EnemyKilled?.Invoke();
        }
    }
}