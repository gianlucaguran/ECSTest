using Svelto.ECS.Example.Survive.Components.Damageable;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using System;
using UnityEngine;


namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class HealthPickupEngine : SingleNodeEngine<HealthPickupNode>, IQueryableNodeEngine
    {

        public HealthPickupEngine(Sequencer sequence)
        {
            _healingSequence = sequence;
        }

        protected override void Add(HealthPickupNode node)
        {
            node.healthPickupComponent.touchPickup += TouchPickUp;
        }

        protected override void Remove(HealthPickupNode node)
        {
            node.removeEntityComponent.removeEntity();
        }

        void TouchPickUp(int entityID, int pickupID)
        {
            var node = nodesDB.QueryNode<HealthPickupNode>(pickupID);
            var healthComponent = node.healthPickupComponent;
            var playerHealInfo = new PlayerHealInfo(healthComponent.healthValue, entityID);
            _healingSequence.Next(this, ref playerHealInfo, DamageCondition.heal);
        }

        Sequencer _healingSequence;

        public IEngineNodeDB nodesDB { set; private get; }
    }


}
