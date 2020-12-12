using UnityEngine;

namespace Components
{
    internal sealed class Movable : MonoBehaviour
    {
        private Transform _transform;
        
        public Vector3 Speed { get; set; }
        
        public void RotateSpeedVector(float deflectionAngle)
        {
            Speed = Quaternion.AngleAxis(deflectionAngle, Vector3.forward) * Speed;
        }

        public void IncreaseSpeedVector(float modifier)
        {
            Speed *= modifier;
        }

        private void Start()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _transform.position += Speed;
        }
    }
}