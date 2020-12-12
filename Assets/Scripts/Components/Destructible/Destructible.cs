using UnityEngine;

namespace Components.Destructible
{
    internal class Destructible : MonoBehaviour
    {
        public bool IsActive { get; set; } = true;
        
        protected virtual void Distraction()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsActive)
            {
                IsActive = false;
            
                Distraction();
            }
        }
    }
}