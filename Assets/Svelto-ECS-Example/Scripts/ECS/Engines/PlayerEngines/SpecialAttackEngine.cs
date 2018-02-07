using System;
using Svelto.ECS.Example.Survive.Nodes.Player;
using UnityEngine;
using Svelto.DataStructures;
using Svelto.ECS.Example.Survive.Nodes.Enemies;

namespace Svelto.ECS.Example.Survive.Engines.Player.Special
{
    public class SpecialAttackEngine : MultiNodesEngine<PlayerNode, EnemyNode>
    {
        public SpecialAttackEngine()
        {
            _enemyList = new FasterList<EnemyNode>(5);
            TaskRunner.Instance.Run(new Tasks.TimedLoopActionEnumerator(Tick));
        }

        void Tick(float deltaSec)
        {
            if (null == _player)
            {
                return;
            }

            var specialAtkComponent = _player.specialAttackComponent;
            bool hasPerformedSpecial = false;

            if (Input.GetButtonDown("Jump"))  //Spacebar 
            {
                if (specialAtkComponent.cooldownTimer > specialAtkComponent.cooldown)
                {
                    PerformSpecialAttack();
                    specialAtkComponent.cooldownTimer = 0.0f;
                    hasPerformedSpecial = true;
                }
                else
                {
                    Debug.Log("Still in CD");
                }
            }

            if (!hasPerformedSpecial)
            {
                specialAtkComponent.cooldownTimer += deltaSec;
            } 
        }

        void PerformSpecialAttack()
        {
            Vector3 distance;
            Vector3 force;
            foreach (EnemyNode node in _enemyList)
            {
                distance = node.transformComponent.transform.position -
                   _player.positionComponent.position;

                if (distance.magnitude <= _player.specialAttackComponent.distance)
                {
                    force = distance.normalized * _player.specialAttackComponent.power;
                    node.rigidBodyComponent.rigidbody.AddForce(force);
                    Debug.Log("pushing " + node.transformComponent.transform.gameObject.name);
                }
            }

        }

        protected override void AddNode(PlayerNode node)
        {
            _player = node;
        }

        protected override void AddNode(EnemyNode node)
        {
            _enemyList.Add(node);
        }

        protected override void RemoveNode(PlayerNode node)
        {
            _player = null;
        }

        protected override void RemoveNode(EnemyNode node)
        {
            _enemyList.Remove(node);
        }


        FasterList<EnemyNode> _enemyList;
        PlayerNode _player;
    }
}