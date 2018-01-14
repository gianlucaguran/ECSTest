using System;
using Svelto.ECS.Example.Survive.Components.Base;
using UnityEngine;


namespace Svelto.ECS.Example.Survive.Implementers.Pickups
{
    public class PickupSoundReaction : MonoBehaviour, ISingleSoundComponent
    {
        //editor variable
        public AudioClip SoundClip;
        //

        AudioSource ISingleSoundComponent.audioSource { get { return _audioSource; } }
        AudioClip ISingleSoundComponent.sound { get { return SoundClip; } }

        // Use this for initialization
        void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
        }

        AudioSource _audioSource;
    }
}
