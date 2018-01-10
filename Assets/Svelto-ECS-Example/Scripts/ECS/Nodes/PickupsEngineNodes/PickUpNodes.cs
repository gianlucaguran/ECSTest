using Svelto.ECS.Example.Survive.Components.Pickups;


namespace Svelto.ECS.Example.Survive.Nodes.Pickups
{
    public class HealthPickupNode : NodeWithID
    {
        public IHealthPickUpComponent healthPickupComponent;
        public IRemoveEntityComponent removeEntityComponent;
    }

    public class AmmoPickupNode : NodeWithID
    {
        public IAmmoPickUpComponent ammoPickupComponent;
    }

}


