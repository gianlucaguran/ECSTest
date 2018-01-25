using UnityEngine;
using Svelto.ECS.Example.Survive.Nodes.Pickups;

namespace Svelto.ECS.Example.Survive.EntityDescriptors.Pickups
{
    class AmmoPickupEntityDescriptor : EntityDescriptor
    {

        static readonly INodeBuilder[] _nodesToBuild;

        static AmmoPickupEntityDescriptor()
        {
            _nodesToBuild = new INodeBuilder[]
            {
                new NodeBuilder<AmmoPickupNode>(),
            };
        }

        public AmmoPickupEntityDescriptor(IComponent[] componentsImplementor) : base(_nodesToBuild, componentsImplementor)
        { }
    }

    [DisallowMultipleComponent]
    public class AmmoPickupEntityDescriptorHolder : MonoBehaviour, IEntityDescriptorHolder
    {
        public EntityDescriptor BuildDescriptorType(object[] extraImplentors = null)
        {
            return new AmmoPickupEntityDescriptor(GetComponentsInChildren<IComponent>());
        }
    }
}