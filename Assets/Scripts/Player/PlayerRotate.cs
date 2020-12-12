using System;
using UnityEngine;

namespace PlayerInput
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerRotate : MonoBehaviour
    {
        private const float RotationSpeed = 5f;
        
        private Rigidbody2D _rigidBody;
    
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }
    
        private void FixedUpdate()
        {
            float xAxis = Input.GetAxis("Horizontal");
            
            if (Math.Abs(xAxis) > 0f)
            {
                OnRotate(xAxis);
            }
        }

        private void OnRotate(float deviation)
        {
            _rigidBody.rotation -= deviation * RotationSpeed;
        }
    }
}