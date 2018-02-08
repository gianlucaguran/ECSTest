using System;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;
using Svelto.ECS.Example.Survive.Nodes.Gun;
using Svelto.ECS.Example.Survive.Components.Pickups;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class AmmoPickupEngine : MultiNodesEngine<AmmoPickupNode, GunNode>, IQueryableNodeEngine, INodeEngine
    {
        
        public AmmoPickupEngine( Sequencer ammoRechargeSequence )
        {
            _ammoRechargeSequence = ammoRechargeSequence;
        }

    

        protected override void AddNode(AmmoPickupNode node)
        {
            node.ammoPickupComponent.touchPickup += TouchPickup;
        }

        protected override void RemoveNode(AmmoPickupNode node)
        {
            node.ammoPickupComponent.touchPickup -= TouchPickup;
            UnityEngine.Object.Destroy(node.transformComponent.transform.gameObject);
        }

        void TouchPickup(int entityID, int pickupID)
        {
            var node = nodesDB.QueryNode<AmmoPickupNode>(pickupID);
            var ammoComponent = node.ammoPickupComponent;
            int ammoCount = ammoComponent.ammoValue;
            _gunNode.ammoHolderComponent.projectilesCount += ammoCount;
            int ammoTotalCount = _gunNode.ammoHolderComponent.projectilesCount;
            AmmoPickupInfo info = new AmmoPickupInfo(entityID, pickupID, ammoTotalCount); 
           _ammoRechargeSequence.Next(this, ref info, Condition.always);

            node.removeEntityComponent.removeEntity();
        }

        protected override void AddNode(GunNode node)
        {
            _gunNode = node;
        }

        protected override void RemoveNode(GunNode node)
        {
            _gunNode = null;
        }

        

        public IEngineNodeDB nodesDB { set; private get; }

        Sequencer _ammoRechargeSequence;
        GunNode _gunNode;
    }
}