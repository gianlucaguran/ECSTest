
using System;
using Svelto.ECS.Example.Survive.Components.Pickups;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Implementers.Player
{
    public class PlayerPickupSounds : MonoBehaviour, IHealthSoundReaction, IAmmoSoundReaction
    {
        //Editor variables
        public AudioClip AmmoSound;
        public AudioClip HealthSound;
        //
        AudioSource IPickUpSoundReaction.audioSource { get { return _audioSource; } } 

        AudioClip IAmmoSoundReaction.sound { get { return AmmoSound; } } 

        AudioClip IHealthSoundReaction.sound { get { return HealthSound; } }
          
        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        AudioSource _audioSource;
    }
}