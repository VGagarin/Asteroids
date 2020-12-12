using UnityEngine;

namespace Components
{
    internal sealed class Blink : MonoBehaviour
    {
        private const float Frequency = 20f;
        private const float MinAlpha = 0.4f;
        private const float MaxAlpha = 1f;
        
        [SerializeField] private SpriteRenderer _sprite;

        private float _duration;
        private bool _isBlinking;
        
        public void StartBlinking(float duration)
        {
            _duration = duration;
            _isBlinking = true;
        }

        private void Update()
        {
            if (_isBlinking)
            {
                _duration -= Time.deltaTime;

                BlinkCycle();
            }
        }

        private void BlinkCycle()
        {
            if (_duration > 0)
            {
                BlinkIteration();
            }
            else
            {
                StopBlinking();
            }
        }

        private void BlinkIteration()
        {
            float alpha = (Mathf.Abs(Mathf.Sin(Time.time * Frequency)) + MinAlpha) / (MinAlpha + MaxAlpha);    
            SetAlpha(alpha);
        }

        private void StopBlinking()
        {
            SetAlpha(MaxAlpha);
            _isBlinking = false;
        }
        
        private void SetAlpha(float alpha)
        {
            Color color = _sprite.color;
            color.a = alpha;

            _sprite.color = color;
        }
    }
}