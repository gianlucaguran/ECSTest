using Svelto.ECS.Example.Survive.Nodes.HUD;
using Svelto.ECS.Example.Survive.Observers.HUD;
using System;

namespace Svelto.ECS.Example.Survive.Engines.HUD
{
    public class AmmoGUIEngine : SingleNodeEngine<HUDNode> , IStep<int>
    {
     
        //method that updates the ammo value in the gui node.
        void UpdateAmmo( ref int ammo )
        {
            _guiNode.ammoComponent.ammo = ammo;
        }

        protected override void Add(HUDNode node)
        {
            _guiNode = node;
        }

        protected override void Remove(HUDNode node)
        {
            _guiNode = null;
        }

        public void Step(ref int token, Enum condition)
        {
            UpdateAmmo(ref token);
        }

        HUDNode _guiNode;
    }
}
