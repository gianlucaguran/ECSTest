using System;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Damageable;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class PickupSoundEngine : MultiNodesEngine<HealthPickupReactionNode>, IQueryableNodeEngine, IStep<PlayerHealInfo>
    {
        public IEngineNodeDB nodesDB { set; private get; }

        

        //entityHealedID not used, kept for callback compatibility purpose
        void PlaySound(int entityHealedID, int nodeID)
        {
            var node = nodesDB.QueryNode<HealthPickupReactionNode>(nodeID);
            //node.soundComponent.audioSource.PlayOneShot(node.soundComponent.sound);
        }

       

        public void Step(ref PlayerHealInfo token, Enum condition)
        {
            PlaySound(token.entityHealed, token.sourceID);
        }

        protected override void AddNode(HealthPickupReactionNode node)
        {
        }

        protected override void RemoveNode(HealthPickupReactionNode node)
        {
        }
    }
}