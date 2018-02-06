using Svelto.ECS.Example.Survive.Nodes.Gun;
using Svelto.ECS.Example.Survive.Nodes.HUD;
using Svelto.ECS.Example.Survive.Observers.HUD;
using System;

namespace Svelto.ECS.Example.Survive.Engines.HUD
{
    public class AmmoGUIEngine : MultiNodesEngine<HUDNode, GunNode>, IStep<int>
    {

        //method that updates the ammo value in the gui node.
        void UpdateAmmo(ref int ammo)
        {
            _guiNode.ammoComponent.ammo = ammo;
        }



        public void Step(ref int token, Enum condition)
        {
            UpdateAmmo(ref token);
        }

        protected override void AddNode(GunNode node)
        {
            int ammo = node.ammoHolderComponent.projectilesCount;
            UpdateAmmo(ref ammo);
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
