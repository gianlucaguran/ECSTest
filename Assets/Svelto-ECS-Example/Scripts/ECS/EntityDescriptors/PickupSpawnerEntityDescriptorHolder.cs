using UnityEngine;
using Svelto.DataStructures;
using System;
using Svelto.ECS.Example.Survive.Components.Pickups;
using Svelto.ECS.Example.Survive.Nodes.Pickups;

namespace Svelto.ECS.Example.Survive.EntityDescriptors.Pickups
{
    class PickupSpawnerEntityDescriptor : EntityDescriptor
    {
        IPickUpSpawnerComponent[] _components;

        public PickupSpawnerEntityDescriptor(IPickUpSpawnerComponent[] componentsImplementor) : base()
        {
            _components = componentsImplementor;
        }
        
        public override FasterList<INode> BuildNodes(int ID)
        {
            var nodes = new FasterList<INode>();
            var node = new PickupSpawningNode
            {
                spawnerComponents = _components
            };

            nodes.Add(node);
            return nodes;
        }
    }

    [DisallowMultipleComponent]
    public class EnemySpawnerEntityDescriptorHolder : MonoBehaviour, IEntityDescriptorHolder
    {
        public EntityDescriptor BuildDescriptorType(object[] extraImplentors = null)
        {
            return new PickupSpawnerEntityDescriptor(GetComponents<IPickUpSpawnerComponent>());
        }
    }
}

