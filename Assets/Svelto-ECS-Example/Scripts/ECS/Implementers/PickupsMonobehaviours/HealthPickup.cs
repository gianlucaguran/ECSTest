using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Pickups;
using System;
using Svelto.ECS.Example.Survive.Components.Base;

namespace Svelto.ECS.Example.Survive.Implementers.Pickups
{
    public class HealthPickup : MonoBehaviour, IHealthPickUpComponent, IRemoveEntityComponent, ITransformComponent, ISingleSoundComponent
    {
        //Editor variables
        public int HealthValue = 10;  //editor variable
        public AudioClip Sound;
        //

        int IHealthPickUpComponent.healthValue { get { return HealthValue; } }

        bool IPickUpComponent.targetInRange { get { return _targetInRange; } set { _targetInRange = value; } }

        Transform ITransformComponent.transform { get { return _transform; } }

        Action IRemoveEntityComponent.removeEntity { get; set; }

        AudioSource ISingleSoundComponent.audioSource { get { return _audioSource; } }

        AudioClip ISingleSoundComponent.sound { get { return Sound; } }

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
            _audioSource = this.GetComponent<AudioSource>();
        }

  
        bool _targetInRange = false;
        Transform _transform;
        AudioSource _audioSource;
    }
}
