using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    [RequireComponent(typeof(Movable))]
    internal sealed class Asteroid : Enemy
    {
        private const int MaxGenerations = 3;
        private const int PiecesCount = 2;
        private const float PartScaleModifier = 0.5f;
        
        private const float MinPartSpeedCoefficient = 1f;
        private const float MaxPartSpeedCoefficient = 1.5f;
        private const float MinPartDeflectionAngle = 15f;
        private const float MaxPartDeflectionAngle = 90f;
        
        private int _generationIndex;

        public void Split()
        {
            CreateAsteroids();
        }

        private void CreateAsteroids()
        {
            if (_generationIndex < MaxGenerations - 1)
            {
                CreateNewGeneration();
            }
        }

        private void CreateNewGeneration()
        {
            for (int i = 0; i < PiecesCount; i++)
            {
                int deflectionDirection = i % 2 == 0 ? -1 : 1;
                CreateAsteroidPart(this, deflectionDirection);
            }
        }

        private void CreateAsteroidPart(Asteroid asteroid, int deflectionDirection)
        {
            Asteroid part = Instantiate(asteroid);
            
            part.transform.localScale *= PartScaleModifier;
            part._generationIndex = _generationIndex + 1;
            part._destroyAction = _destroyAction;

            ChangePartSpeed(part, deflectionDirection);
        }

        private void ChangePartSpeed(Asteroid part, int deflectionDirection)
        {
            float speedModifier = Random.Range(MinPartSpeedCoefficient, MaxPartSpeedCoefficient);
            float deflectionAngle = Random.Range(MinPartDeflectionAngle, MaxPartDeflectionAngle);
            
            Movable movableComponent = part.GetComponent<Movable>();
            
            movableComponent.Speed = GetComponent<Movable>().Speed;
            movableComponent.IncreaseSpeedVector(speedModifier);
            movableComponent.RotateSpeedVector(deflectionAngle * deflectionDirection);
        }
    }
}