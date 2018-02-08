using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Player;
using System;

namespace Svelto.ECS.Example.Survive.Implementers.Player
{

    public class PlayerSpecialAtkSounds : MonoBehaviour, ISpecialAtkSoundComponent
    {
        //Editor variables
        public AudioClip AttackClip;
        public AudioClip CooldownFailClip;
        //

        AudioSource ISpecialAtkSoundComponent.audioSource { get { return _audioSource; } }

        AudioClip ISpecialAtkSoundComponent.failClip { get { return CooldownFailClip; } }

        AudioClip ISpecialAtkSoundComponent.performClip { get { return AttackClip; } }

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        AudioSource _audioSource;
    }
}