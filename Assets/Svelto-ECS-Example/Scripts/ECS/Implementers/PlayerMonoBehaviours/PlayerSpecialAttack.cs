using Svelto.ECS.Example.Survive.Components.Player;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Implementers.Player
{

    public class PlayerSpecialAttack : MonoBehaviour, ISpecialAttackComponent
    {
        //Editor variables - special attack configurable parameters
        public float Power = 50.0f;
        public float Cooldown = 10.0f;
        public float SquaredDistance = 5.0f;
        //
        float ISpecialAttackComponent.cooldown { get { return Cooldown; } }

        float ISpecialAttackComponent.sqrDistance { get { return SquaredDistance; } }

        float ISpecialAttackComponent.power { get { return Power; } }

        //timer that increases each tick  
        float ISpecialAttackComponent.cooldownTimer { get { return _cooldownTimer; } set { _cooldownTimer = value; } }
         
        float _cooldownTimer = 0.0f;
    }
}
