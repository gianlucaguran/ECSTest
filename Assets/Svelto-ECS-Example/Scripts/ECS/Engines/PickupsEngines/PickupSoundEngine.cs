using System;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;
using UnityEngine;
using Svelto.ECS.Example.Survive.Components.Damageable;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class PickupSoundEngine : MultiNodesEngine<HealthPickupReactionNode, AmmoPickupReactionNode>, IQueryableNodeEngine, IStep<PlayerHealInfo>, IStep<int>
    {
        public IEngineNodeDB nodesDB { set; private get; }



        //nodeID not used, kept for callback compatibility purpose
        void PlaySound(int entityHealedID, int nodeID)
        {
            var node = nodesDB.QueryNode<HealthPickupReactionNode>(entityHealedID);
            node.soundComponent.audioSource.PlayOneShot(node.soundComponent.sound);
        }

       

        public void Step(ref PlayerHealInfo token, Enum condition)
        {
            Debug.Log("Step called from pickupsoundengine");
            PlaySound(token.entityHealed, token.sourceID);
        }

        protected override void AddNode(HealthPickupReactionNode node)
        { 
        }

        protected override void RemoveNode(HealthPickupReactionNode node)
        { 
        }

        public void Step(ref int token, Enum condition)
        { 
        }

        protected override void AddNode(AmmoPickupReactionNode node)
        { 
        }

        protected override void RemoveNode(AmmoPickupReactionNode node)
        { 
        }
    }
}