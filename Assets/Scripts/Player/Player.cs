using System;
using Components;
using Components.Destructible;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(Blink))]
    internal sealed class Player : MonoBehaviour
    {
        private const float ImmortalityDuration = 3f;
        
        public event Action Died;

        private float _timeOfLife;
        private bool _immortalityActive;
        private Destructible _destructibleComponent;
        private Blink _blinkComponent;

        private void Awake()
        {
            _destructibleComponent = GetComponent<Destructible>();
            _blinkComponent = GetComponent<Blink>();
        }

        private void Start()
        {
            _destructibleComponent.IsActive = false;
            _blinkComponent.StartBlinking(ImmortalityDuration);
            _immortalityActive = true;
        }

        private void Update()
        {
            _timeOfLife += Time.deltaTime;

            if (_immortalityActive && _timeOfLife > ImmortalityDuration)
            {
                _destructibleComponent.IsActive = true;
                _immortalityActive = false;
            }
        }

        private void OnDestroy()
        {
            Died?.Invoke();
        }
    }
}
