using System;
using UnityEngine;

namespace Components
{
    internal abstract class Enemy : MonoBehaviour
    {
        protected Action _destroyAction;
        
        public void SetDestroyAction(Action destroyAction)
        {
            _destroyAction = destroyAction;
        }
        
        private void OnDestroy()
        {
            _destroyAction?.Invoke();
        }
    }
}