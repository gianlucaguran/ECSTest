using System;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Enemy;

namespace Svelto.ECS.Example.Survive.Implementers.Enemies
{
    public class EnemyTrigger : MonoBehaviour, IEnemyTriggerComponent, IEnemyAttackComponent
    {
        public GameObject ID { get { return gameObject; } }

        public event Action<int, int, bool> entityInRange;

        bool IEnemyTriggerComponent.targetInRange { set { _targetInRange = value; } }
        bool IEnemyAttackComponent.targetInRange { get { return _targetInRange; } }

        void OnTriggerEnter(Collider other)
        {
            if (entityInRange != null)
                entityInRange(other.gameObject.GetInstanceID(), gameObject.GetInstanceID(), true);
        }

        void OnTriggerExit(Collider other)
        {
            if (entityInRange != null)
                entityInRange(other.gameObject.GetInstanceID(), gameObject.GetInstanceID(), false);
        }

        bool    _targetInRange;
    }
}
