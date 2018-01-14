using System;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class PickupSoundEngine : MultiNodesEngine<PickupSoundNode>, INodeEngine, IQueryableNodeEngine
    {
        public IEngineNodeDB nodesDB { set; private get; }

         void INodeEngine.Add(INode node)
        {
            var component = (node as PickupSoundNode).healthPickupComponent;
            component.touchPickup += PlaySound;
        }

         void INodeEngine.Remove(INode node)
        {
            var component = (node as PickupSoundNode).healthPickupComponent;
            component.touchPickup -= PlaySound;
        }

        //entityHealedID not used, kept for callback compatibility purpose
        void PlaySound(int entityHealedID, int nodeID)
        {
            var node = nodesDB.QueryNode<PickupSoundNode>(nodeID);
            node.soundComponent.audioSource.PlayOneShot(node.soundComponent.sound);
        }

        protected override void AddNode(PickupSoundNode node)
        {
            Debug.Log("Add Node Called");
        }

        protected override void RemoveNode(PickupSoundNode node)
        {
            Debug.Log("Remove Node Called");
        }
    }
}