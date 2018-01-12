using Svelto.ECS.Example.Survive.Components.Damageable;
using Svelto.ECS.Example.Survive.Nodes.DamageableEntities;
using System;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Engines.Health
{
    public class HealthEngine : IEngine, IQueryableNodeEngine, IStep<DamageInfo>, IStep<PlayerDamageInfo>, IStep<PlayerHealInfo>
    {
        Sequencer _damageSequence;
        Sequencer _healSequence;

        public HealthEngine(Sequencer playerDamageSequence, Sequencer healingSequence )
        {
            _damageSequence = playerDamageSequence;
            _healSequence = healingSequence;
        }

        public HealthEngine(Sequencer playerDamageSequence) : this ( playerDamageSequence, null)
        {}

        public IEngineNodeDB nodesDB { set; private get; }

        public void Step(ref PlayerDamageInfo token, Enum condition)
        {
            TriggerDamage(ref token);
        }

        public void Step(ref DamageInfo token, Enum condition)
        {
            TriggerDamage(ref token);
        }

        public void Step(ref PlayerHealInfo token, Enum condition)
        {
            Heal(ref token);
        }

        void TriggerDamage<T>(ref T damage) where T:IDamageInfo
        {
            var node = nodesDB.QueryNode<HealthNode>(damage.entityDamaged);
            var healthComponent = node.healthComponent;

            healthComponent.currentHealth -= damage.damagePerShot;

            if (healthComponent.currentHealth <= 0)
            {
                _damageSequence.Next(this, ref damage, DamageCondition.dead);
                node.removeEntityComponent.removeEntity();
            }
            else
                _damageSequence.Next(this, ref damage, DamageCondition.damage);
        }

        //adding method to heal 
        void Heal<T> (ref T heal) where T: IHealInfo
        {
            var node = nodesDB.QueryNode<HealthNode>(heal.entityHealed);
            var healthComponent = node.healthComponent;

           
            int health = healthComponent.currentHealth + heal.HPGained;
            healthComponent.currentHealth = Math.Min(health, healthComponent.maxHealth);

            _healSequence.Next(this, ref heal, DamageCondition.heal);
        }
    }
}
