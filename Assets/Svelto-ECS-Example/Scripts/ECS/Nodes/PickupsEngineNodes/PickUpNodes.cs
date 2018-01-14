using Svelto.ECS.Example.Survive.Components.Base;
using Svelto.ECS.Example.Survive.Components.Pickups;


namespace Svelto.ECS.Example.Survive.Nodes.Pickups
{
    public class HealthPickupNode : NodeWithID
    {
        public IHealthPickUpComponent healthPickupComponent;
        public ITransformComponent transformComponent;  //this is necessary to access gameobject and remove the pickup for the scene
        public IRemoveEntityComponent removeEntityComponent;
    }

    public class AmmoPickupNode : NodeWithID
    {
        public IAmmoPickUpComponent ammoPickupComponent;
        public ITransformComponent transformComponent;
        public IRemoveEntityComponent removeEntityComponent;
    }

    public class PickupSpawningNode : NodeWithID
    {
        public IPickUpSpawnerComponent[] spawnerComponents;
    }

    public class PickupSoundNode : NodeWithID
    {
        public ISingleSoundComponent soundComponent;
        public IHealthPickUpComponent healthPickupComponent;
    }
}


