using UnityEngine;

namespace Components.Destructible
{
    internal class DestructibleWithParticleEffect : Destructible
    {
        [SerializeField] private ParticleSystem _destructionEffect;

        protected override void Distraction()
        {
            Instantiate(_destructionEffect, transform.position, Quaternion.identity);
            
            base.Distraction();
        }
    }
}