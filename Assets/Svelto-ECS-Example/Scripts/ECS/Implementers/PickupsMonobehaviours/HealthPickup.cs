using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Pickups;
using System;

namespace Svelto.ECS.Example.Survive.Implementers.Player
{
    public class HealthPickup : MonoBehaviour, IHealthPickUpComponent, IRemoveEntityComponent
    {
        public int HealthValue = 10;  //editor variable

        int IHealthPickUpComponent.healthValue { get { return HealthValue; } }

        bool IPickUpComponent.targetInRange { get { return _targetInRange; } set { _targetInRange = value; } }

        Action IRemoveEntityComponent.removeEntity { get; set; }

        public event Action<int, int> touchPickup;

        //only player can trigger these events because it's the only thing that can collide with pickups 
        //( see physics settings) 
        // maybe i can remove id parameters ? 
         void OnTriggerEnter(Collider other)
        {
            if (null != touchPickup)
            {
                touchPickup(other.GetInstanceID(), this.GetInstanceID());
            }
        }

        // void OnTriggerExit(Collider other)
        //{
        //    if (null != touchPickup)
        //    {
        //        touchPickup(other.GetInstanceID(), this.GetInstanceID());
        //    }
        //}
        
        bool _targetInRange = false;
    }
}
