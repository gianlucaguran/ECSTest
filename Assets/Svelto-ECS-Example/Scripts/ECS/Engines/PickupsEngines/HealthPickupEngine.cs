using System;
using Svelto.ECS.Example.Survive.Components.Damageable;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class HealthPickupEngine : MultiNodesEngine<HealthPickupNode>, IQueryableNodeEngine, INodeEngine
    {

        public HealthPickupEngine(Sequencer sequence)
        {
            _healingSequence = sequence;
        }

        protected override void AddNode(HealthPickupNode node)
        {
            node.healthPickupComponent.touchPickup += TouchPickUp;
        }

        protected override void RemoveNode(HealthPickupNode node)
        {
            node.healthPickupComponent.touchPickup -= TouchPickUp;
        }

        void TouchPickUp(int entityID, int pickupID)
        {
            var node = nodesDB.QueryNode<HealthPickupNode>(pickupID);
            var healthComponent = node.healthPickupComponent;
            var playerHealInfo = new PlayerHealInfo(healthComponent.healthValue, entityID, pickupID);
            _healingSequence.Next(this, ref playerHealInfo, DamageCondition.heal);

            //node.removeEntityComponent.removeEntity();
        }

        public void Add(INode obj)
        {
            (obj as HealthPickupNode).healthPickupComponent.touchPickup += TouchPickUp;
        }

        public void Remove(INode obj)
        {
            HealthPickupNode node = obj as HealthPickupNode;
            node.healthPickupComponent.touchPickup -= TouchPickUp;
            UnityEngine.Object.Destroy(node.transformComponent.transform.gameObject);
        }


        Sequencer _healingSequence;

        public IEngineNodeDB nodesDB { set; private get; }
    }


}
