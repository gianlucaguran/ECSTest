using UnityEngine;
using Svelto.ECS.Example.Survive.Nodes.Pickups;

namespace Svelto.ECS.Example.Survive.EntityDescriptors.Pickups
{
    class HealthPickupEntityDescriptor : EntityDescriptor
    {
        static readonly INodeBuilder[] _nodesToBuild;

        static HealthPickupEntityDescriptor()
        {
            _nodesToBuild = new INodeBuilder[]
            {
                new NodeBuilder<HealthPickupNode>(),
                new NodeBuilder<PickupSoundNode>()
            };
        }

        public HealthPickupEntityDescriptor(IComponent[] componentsImplementor) : base(_nodesToBuild, componentsImplementor)
        { }
    }

    [DisallowMultipleComponent]
    public class HealthPickupEntityDescriptorHolder : MonoBehaviour, IEntityDescriptorHolder
    {
        public EntityDescriptor BuildDescriptorType(object[] extraImplentors = null)
        {
            return new HealthPickupEntityDescriptor(GetComponentsInChildren<IComponent>());
        }
    }
}