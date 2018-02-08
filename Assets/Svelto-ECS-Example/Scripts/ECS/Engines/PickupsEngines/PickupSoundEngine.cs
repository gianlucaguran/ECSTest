using System;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;
using UnityEngine; 
using Svelto.ECS.Example.Survive.Components.Pickups;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public abstract class PickupSoundEngine<T, U, W> : MultiNodesEngine<T>, IStep<W>
        where T : IPickupReactionNode<U>
        where U : IPickUpSoundReaction
        where W : IPickupInfo

    {
        public abstract void Step(ref W token, Enum condition);
    }


    public class HealthPickupSoundEngine : PickupSoundEngine<HealthPickupReactionNode, IHealthSoundReaction, HealthPickupInfo>,
        IQueryableNodeEngine 
    {
        public IEngineNodeDB nodesDB { set; private get; }

        public override void Step(ref HealthPickupInfo token, Enum condition)
        {
            var node = nodesDB.QueryNode<HealthPickupReactionNode>(token.entity);
            node.soundComponent.audioSource.PlayOneShot(node.soundComponent.sound);
        }

        protected override void AddNode(HealthPickupReactionNode node)
        {
        }

        protected override void RemoveNode(HealthPickupReactionNode node)
        {
        }
    }

    public class AmmoPickupSoundEngine : PickupSoundEngine<AmmoPickupReactionNode, IAmmoSoundReaction, AmmoPickupInfo>, 
        IQueryableNodeEngine 
    {
        public IEngineNodeDB nodesDB { set; private get; }

        public override void Step(ref AmmoPickupInfo token, Enum condition)
        {
            var node = nodesDB.QueryNode<AmmoPickupReactionNode>(token.entity);
            node.soundComponent.audioSource.PlayOneShot(node.soundComponent.sound);
        }

        protected override void AddNode(AmmoPickupReactionNode node)
        {
        }

        protected override void RemoveNode(AmmoPickupReactionNode node)
        {
        }
    }
}