using Components;
using UnityEngine;

namespace PlayerInput
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerFiring : MonoBehaviour
    {
        private const float FireDelay = 0.2f;
        private const float BulletSpeed = 0.3f;
        
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _spawnPoint;

        private float _timeAfterLastFire;
        private Rigidbody2D _rigidBody;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            _timeAfterLastFire += Time.fixedDeltaTime;
            
            if (Input.GetAxis("Fire1") > 0 && _timeAfterLastFire > FireDelay)
            {
                OnFire();
            }
        }

        private void OnFire()
        {
            _timeAfterLastFire = 0;
            
            float deviationAngle = _rigidBody.rotation;

            Vector2 direction = new Vector2(
                Mathf.Cos(deviationAngle * Mathf.Deg2Rad),
                Mathf.Sin(deviationAngle * Mathf.Deg2Rad));

            Bullet newBullet = Instantiate(_bullet, _spawnPoint.position, _spawnPoint.rotation);
            newBullet.Initialize(direction * BulletSpeed);
        }
    }
}