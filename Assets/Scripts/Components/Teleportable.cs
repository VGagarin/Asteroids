using Management;
using UnityEngine;

namespace Components
{
    internal sealed class Teleportable : MonoBehaviour
    {
        private void Start()
        {
            Teleport.Register(this);
        }

        private void OnDestroy()
        {
            Teleport.Remove(this);
        }
    }
}