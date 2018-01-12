using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Pickups;
using System;
using Svelto.ECS.Example.Survive.Components.Base;

namespace Svelto.ECS.Example.Survive.Implementers.Pickups
{
    public class HealthPickup : MonoBehaviour, IHealthPickUpComponent, IRemoveEntityComponent, ITransformComponent
    {
        public int HealthValue = 10;  //editor variable

        int IHealthPickUpComponent.healthValue { get { return HealthValue; } }

        bool IPickUpComponent.targetInRange { get { return _targetInRange; } set { _targetInRange = value; } }

        Transform ITransformComponent.transform { get { return _transform; } }

        Action IRemoveEntityComponent.removeEntity { get; set; }

        public event Action<int, int> touchPickup;

        //only player can trigger these events because it's the only thing that can collide with pickups 
        //( see physics settings) 
         void OnTriggerEnter(Collider other)
        {
            if (null != touchPickup)
            {
                touchPickup(other.gameObject.GetInstanceID(), this.gameObject.GetInstanceID());
            }
        }

        private void Awake()
        {
            _transform = this.transform;
        }

  
        bool _targetInRange = false;
        Transform _transform;
    }
}
