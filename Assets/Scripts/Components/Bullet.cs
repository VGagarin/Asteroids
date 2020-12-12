using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Movable))]
    internal sealed class Bullet : MonoBehaviour
    {
        private const float MaxTimeOfLife = 0.7f;
        
        private float _currentTimeOfLife;
        
        public void Initialize(Vector2 speed)
        {
            GetComponent<Movable>().Speed = speed;
        }
        
        public void Initialize(Vector2 speed, float deflectionAngle)
        {
            Movable movable = GetComponent<Movable>();
            
            movable.Speed = speed;
            movable.RotateSpeedVector(deflectionAngle);
        }

        private void FixedUpdate()
        {
            _currentTimeOfLife += Time.fixedDeltaTime;

            if (_currentTimeOfLife >= MaxTimeOfLife)
            {
                Destroy(gameObject);
            }
        }
    }
}