using System;
using Svelto.DataStructures;
using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.Tasks;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class RotatingIdlePickupsEngine : MultiNodesEngine<HealthPickupNode, AmmoPickupNode>, IQueryableNodeEngine
    {
        public IEngineNodeDB nodesDB { set; private get; }

        public RotatingIdlePickupsEngine()
        {
            _ammoList = new FasterList<AmmoPickupNode>(10);
            _heartList = new FasterList<HealthPickupNode>(10);
            TaskRunner.Instance.Run(new TimedLoopActionEnumerator(Tick));
        }

        void Tick(float deltaSec)
        {
            Vector3 up = Vector3.up;

            foreach (var heart in _heartList)
            {
                heart.transformComponent.transform.Rotate(up, _heartRotationSpeed * deltaSec);
            }


            foreach (var ammo in _ammoList)
            {
                ammo.transformComponent.transform.Rotate(up, _ammoRotationSpeed * deltaSec);
            }
        }

        protected override void AddNode(HealthPickupNode node)
        { 
            _heartList.Add(node);
        }

        protected override void RemoveNode(HealthPickupNode node)
        {
            _heartList.Remove(node);
        }

        protected override void AddNode(AmmoPickupNode node)
        { 
            _ammoList.Add(node);
        }

        protected override void RemoveNode(AmmoPickupNode node)
        {
            _ammoList.Remove(node);
        }

        FasterList<AmmoPickupNode> _ammoList;
        FasterList<HealthPickupNode> _heartList;
        const float _heartRotationSpeed = 30.0f;
        const float _ammoRotationSpeed = 30.0f;
    }
}
