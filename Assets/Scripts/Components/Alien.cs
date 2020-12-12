using UnityEngine;

namespace Components
{
    internal sealed class Alien : Enemy
    {
        private const float MinFireDelay = 0.5f;
        private const float MaxFireDelay = 1f;
        private const float DeviationAngleAmplitude = 30f;
        private const float BulletSpeed = 0.3f;
        
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _spawnPoint;

        private float _fireDelay;

        private void FixedUpdate()
        {
            _fireDelay -= Time.fixedDeltaTime;
            
            if (_fireDelay < 0f)
            {
                _fireDelay = Random.Range(MinFireDelay, MaxFireDelay);
                
                OnFire();
            }
        }

        private void OnFire()
        {
            float deviationAngle = Random.Range(-DeviationAngleAmplitude, DeviationAngleAmplitude);

            Bullet newBullet = Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
            
            newBullet.Initialize(Vector3.down * BulletSpeed, deviationAngle);
        }
    }
}