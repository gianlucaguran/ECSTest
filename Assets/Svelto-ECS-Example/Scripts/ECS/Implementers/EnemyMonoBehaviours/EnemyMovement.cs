using System;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Enemy;
using Svelto.ECS.Example.Survive.Components.Base;

namespace Svelto.ECS.Example.Survive.Implementers.Enemies
{
    public class EnemyMovement : MonoBehaviour, IEnemyMovementComponent, ITransformComponent, IRigidBodyComponent
    {
        public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.

        UnityEngine.AI.NavMeshAgent    _nav;                       // Reference to the nav mesh agent.
        CapsuleCollider _capsuleCollider;           // Reference to the capsule collider.
        Transform       _transform;
        Action          _removeAction;
        Rigidbody       _rigidBody;

        CapsuleCollider IEnemyMovementComponent.capsuleCollider { get { return _capsuleCollider; } }
        UnityEngine.AI.NavMeshAgent IEnemyMovementComponent.navMesh { get { return _nav; } }

        float IEnemyMovementComponent.sinkSpeed { get { return sinkSpeed; } }
        Transform ITransformComponent.transform { get { return _transform; }}
        Rigidbody IRigidBodyComponent.rigidbody { get { return _rigidBody; }} 

        public Action removeEntity { get { return _removeAction; } set { _removeAction = value;  }  }

       

        void Awake ()
        {
            _nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = transform;
            _rigidBody = GetComponent<Rigidbody>();
        }
    }
}
