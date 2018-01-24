using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Pickups;
using Svelto.ECS.Example.Survive.Components.Base;
using System;

namespace Svelto.ECS.Example.Survive.Implementers.Pickups
{

    public class AmmoPickup : MonoBehaviour, IAmmoPickUpComponent, ITransformComponent, IRemoveEntityComponent
    {
        //Editor variables
        public int AmmoValue = 10;
        //

        Action IRemoveEntityComponent.removeEntity {  get; set; }

        Transform ITransformComponent.transform { get { return _transform; } }

        int IAmmoPickUpComponent.ammoValue { get { return AmmoValue; } }

        bool IPickUpComponent.targetInRange { get; set; }

        public event Action<int, int> touchPickup ;


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
