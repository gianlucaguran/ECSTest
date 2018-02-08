using Svelto.ECS.Example.Survive.Components.Damageable;
using Svelto.ECS.Example.Survive.Components.Pickups;
using Svelto.ECS.Example.Survive.Nodes.HUD;
using System;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Engines.HUD
{
    public class HUDEngine : SingleNodeEngine<HUDNode>, IQueryableNodeEngine, IStep<PlayerDamageInfo> , IStep<HealthPickupInfo>
    {
        public IEngineNodeDB nodesDB { set; private get; }

        public HUDEngine()
        {
            TaskRunner.Instance.Run(new Tasks.TimedLoopActionEnumerator(Tick));
        }

        protected override void Add(HUDNode node)
        {
            _guiNode = node;
        }

        protected override void Remove(HUDNode node)
        {
            _guiNode = null;
        }

        void Tick(float deltaSec)
        {
            if (_guiNode == null) return;

            var damageComponent = _guiNode.damageImageComponent;
            var damageImage = damageComponent.damageImage;

            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageComponent.flashSpeed * deltaSec);
        }

        void OnDamageEvent(ref PlayerDamageInfo damaged)
        {
            var damageComponent = _guiNode.damageImageComponent;
            var damageImage = damageComponent.damageImage;

            damageImage.color = damageComponent.flashColor;

            _guiNode.healthSliderComponent.healthSlider.value = nodesDB.QueryNode<HUDDamageEventNode>(damaged.entityDamaged).healthComponent.currentHealth;
        }

        void OnDeadEvent()
        {
            _guiNode.HUDAnimator.hudAnimator.SetTrigger("GameOver");
        }

        public void Step(ref PlayerDamageInfo token, Enum condition)
        {
            if ((DamageCondition)condition == DamageCondition.damage)
                OnDamageEvent(ref token);
            else
            if ((DamageCondition)condition == DamageCondition.dead)
                OnDeadEvent();
                
        }

        public void Step( ref HealthPickupInfo token, Enum condition )
        { 
            if ((DamageCondition)condition == DamageCondition.heal)
            {
                OnHealEvent(ref token);
            }
        }

        //Healing event
        //similar to damage event but increases value of the slider, and doesn't flash the damage image
        void OnHealEvent( ref HealthPickupInfo token )
        {  
            _guiNode.healthSliderComponent.healthSlider.value = nodesDB.QueryNode<HUDDamageEventNode>(token.entity).healthComponent.currentHealth;
        }

        HUDNode         _guiNode;
    }
}

