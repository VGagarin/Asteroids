using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Management
{
    internal sealed class Teleport : MonoBehaviour
    {
        private static readonly List<Teleportable> _objects = new List<Teleportable>();

        public static void Register(Teleportable teleportable)
        {
            _objects.Add(teleportable);
        }

        public static void Remove(Teleportable teleportable)
        {
            _objects.Remove(teleportable);
        }

        [SerializeField] private Transform _upBound;
        [SerializeField] private Transform _downBound;
        [SerializeField] private Transform _leftBound;
        [SerializeField] private Transform _rightBound;

        private void FixedUpdate()
        {
            foreach (Teleportable teleportable in _objects)
            {
                TeleportObjectIfNeeded(teleportable.transform);
            }
        }

        private void TeleportObjectIfNeeded(Transform objectTransform)
        {
            Vector3 position = objectTransform.position;

            if (CheckHorizontalAxis(ref position) | CheckVerticalAxis(ref position))
            {
                objectTransform.position = position;
            }
        }

        private bool CheckVerticalAxis(ref Vector3 position)
        {
            if (position.y > _upBound.position.y)
            {
                position.y = _downBound.position.y;
                return true;
            }

            if (position.y < _downBound.position.y)
            {
                position.y = _upBound.position.y;
                return true;
            }

            return false;
        }
        
        private bool CheckHorizontalAxis(ref Vector3 position)
        {
            if (position.x > _rightBound.position.x)
            {
                position.x = _leftBound.position.x;
                return true;
            }

            if (position.x < _leftBound.position.x)
            {
                position.x = _rightBound.position.x;
                return true;
            }

            return false;
        }
    }
}