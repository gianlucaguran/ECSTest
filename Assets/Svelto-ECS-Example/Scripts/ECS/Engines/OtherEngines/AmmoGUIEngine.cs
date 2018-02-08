using Svelto.ECS.Example.Survive.Components.Pickups;
using Svelto.ECS.Example.Survive.Nodes.Gun;
using Svelto.ECS.Example.Survive.Nodes.HUD;
using System;

namespace Svelto.ECS.Example.Survive.Engines.HUD
{
    public class AmmoGUIEngine : MultiNodesEngine<HUDNode, GunNode>, IStep<AmmoPickupInfo>, IStep<int>
    {

        //method that updates the ammo value in the gui node.
        void UpdateAmmo( int ammo)
        {
            _guiNode.ammoComponent.ammo = ammo;
        }
         
        public void Step(ref AmmoPickupInfo token, Enum condition)
        { 
            UpdateAmmo(token.value);
        }

        public void Step(ref int token, Enum condition)
        {
            UpdateAmmo(token);
        }

        protected override void AddNode(GunNode node)
        {
            int ammo = node.ammoHolderComponent.projectilesCount;
            UpdateAmmo( ammo);
        }

        protected override void RemoveNode(GunNode node)
        {
            
        }

        protected override void AddNode(HUDNode node)
        {
            _guiNode = node;
        }

        protected override void RemoveNode(HUDNode node)
        {
            _guiNode = null;
        }

        

        HUDNode _guiNode;
    }
}
