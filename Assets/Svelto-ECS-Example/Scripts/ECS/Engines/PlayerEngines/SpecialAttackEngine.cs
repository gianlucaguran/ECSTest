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

        public SpecialAttackEngine(Sequencer specialAtkFXSequence) : this()
        {
            _specialAtkFXSequence = specialAtkFXSequence;
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
                    SpecialAttackCondition param = SpecialAttackCondition.fail;
                    _specialAtkFXSequence.Next(this, ref param, SpecialAttackCondition.fail);
                }
            }

            if (!hasPerformedSpecial)
            {
                specialAtkComponent.cooldownTimer += deltaSec;
            } 
        }

        void PerformSpecialAttack()
        {
            SpecialAttackCondition param = SpecialAttackCondition.perform;
            _specialAtkFXSequence.Next(this, ref param, SpecialAttackCondition.perform); 

            Vector3 distance;
            Vector3 force;
            foreach (EnemyNode node in _enemyList)
            {
                distance = node.transformComponent.transform.position -
                   _player.positionComponent.position;

                if (distance.sqrMagnitude <= _player.specialAttackComponent.sqrDistance)
                {
                    force = distance.normalized * _player.specialAttackComponent.power;
                    node.rigidBodyComponent.rigidbody.AddForce(force);  
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
        Sequencer _specialAtkFXSequence;
    }
}