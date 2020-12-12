using UnityEngine;

namespace Components.Destructible
{
    [RequireComponent(typeof(Asteroid))]
    internal sealed class AsteroidDestructible : DestructibleWithParticleEffect
    {
        protected override void Distraction()
        {
            GetComponent<Asteroid>().Split();

            base.Distraction();
        }
    }
}