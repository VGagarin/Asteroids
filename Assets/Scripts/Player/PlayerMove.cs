using UnityEngine;

namespace PlayerInput
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerMove : MonoBehaviour
    {
        private const float MoveSpeed = 10f;
        
        private Rigidbody2D _rigidBody;
    
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }
    
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                OnMove();
            }
        }

        private void OnMove()
        {
            float deviationAngle = _rigidBody.rotation;

            Vector2 direction = new Vector2(
                Mathf.Cos(deviationAngle * Mathf.Deg2Rad),
                Mathf.Sin(deviationAngle * Mathf.Deg2Rad));
                
            _rigidBody.AddForce(direction * MoveSpeed);
        }
    }
}