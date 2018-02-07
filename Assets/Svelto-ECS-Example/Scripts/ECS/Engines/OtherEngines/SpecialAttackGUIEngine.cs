using System;
using Svelto.ECS.Example.Survive.Nodes.HUD;
using Svelto.ECS.Example.Survive.Nodes.Player;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Engines.HUD
{
    public class SpecialAttackGUIEngine : MultiNodesEngine<PlayerNode, HUDNode>
    {

       public SpecialAttackGUIEngine()
        {
            TaskRunner.Instance.Run(new Tasks.TimedLoopActionEnumerator(Tick));
        }

        void Tick(float deltaSec)
        {
            if ( null == _player || null == _HUD)
            {
                return;
            }

            float currentTimer = _player.specialAttackComponent.cooldownTimer;
            float cooldown = _player.specialAttackComponent.cooldown;
            float valueCharge = Mathf.Min(currentTimer, cooldown) / cooldown;

            _HUD.specialBarComponent.bar.value = valueCharge;

            if ( 1.0f == valueCharge && 
                ! _HUD.specialBarComponent.readyIcon.activeInHierarchy )
            {
                _HUD.specialBarComponent.readyIcon.SetActive(true);
            }
            else if ( 1.0f > valueCharge &&
                _HUD.specialBarComponent.readyIcon.activeInHierarchy)
            {
                _HUD.specialBarComponent.readyIcon.SetActive(false);
            }
        }

        protected override void AddNode(PlayerNode node)
        {
            _player = node;
        }

        protected override void AddNode(HUDNode node)
        {
            _HUD = node;
        }

        protected override void RemoveNode(PlayerNode node)
        {
            _player = null;
        }

        protected override void RemoveNode(HUDNode node)
        {
            _HUD = null;
        }

        PlayerNode _player;
        HUDNode _HUD;
    }
}
