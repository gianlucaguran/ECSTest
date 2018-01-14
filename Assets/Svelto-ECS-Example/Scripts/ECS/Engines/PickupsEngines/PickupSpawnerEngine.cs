using Svelto.ECS.Example.Survive.Nodes.Pickups;
using Svelto.ECS.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Svelto.ECS.Example.Survive.Components.Pickups;
using Svelto.Tasks;
using Svelto.DataStructures;

namespace Svelto.ECS.Example.Survive.Engines.Pickups
{
    public class PickupSpawnerEngine : MultiNodesEngine<PickupSpawningNode> , INodeEngine
    {
        internal class PickupSpawnerData
        {
            internal GameObject bonus;
            internal Transform[] spawnPoints;
            internal float time;
            internal float probability;
            internal float timer; //this way each spawner can count its own time

            
            internal PickupSpawnerData(IPickUpSpawnerComponent spawnerComponent)
            {
                bonus = spawnerComponent.pickupPrefab;
                spawnPoints = spawnerComponent.spawnPoints;
                time = spawnerComponent.spawnTime;
                probability = spawnerComponent.spawnProbability;
                timer = 0.0f;
            }
        }

        public PickupSpawnerEngine(Factories.IGameObjectFactory factory, IEntityFactory entityFactory)
        {
            _factory = factory;
            _entityFactory = entityFactory;
            TaskRunner.Instance.Run(new TimedLoopActionEnumerator(Tick));
        }

        void Tick(float deltaTime)
        {
            
            for (int i = 0; _bonusToSpawn.Count > i; i++)
            {
                if (_bonusToSpawn[i].timer > _bonusToSpawn[i].time)
                {
                    PickupSpawnerData data = _bonusToSpawn[i];
                    SpawnBonus(ref data);
                    _bonusToSpawn[i] = data;
                }

                else
                {
                    _bonusToSpawn[i].timer += deltaTime;
                }
            }
        }

        void SpawnBonus(ref PickupSpawnerData spawnData)
        {
            foreach (Transform t in spawnData.spawnPoints)
            {
                if(UnityEngine.Random.value < spawnData.probability)
                {
                    var go = _factory.Build(spawnData.bonus);
                    _entityFactory.BuildEntity(go.GetInstanceID(), go.GetComponent<IEntityDescriptorHolder>().BuildDescriptorType());
                    var transform = go.transform;
                    transform.position = t.position;
                    transform.rotation = t.rotation;
                }
            }

            spawnData.timer = 0;
        }

        protected override void AddNode(PickupSpawningNode node)
        {
           
        }

        protected override void RemoveNode(PickupSpawningNode node)
        {

        }

        public void Add(INode obj)
        {
            var node = obj as PickupSpawningNode;

            var spawnerComponents = node.spawnerComponents;

            for (int i = 0; i < spawnerComponents.Length; i++)
            {
                _bonusToSpawn.Add(new PickupSpawnerData(spawnerComponents[i]));
            }
        }

        public void Remove(INode obj)
        {
            
        }

        FasterList<PickupSpawnerData> _bonusToSpawn = new FasterList<PickupSpawnerData>();
        Svelto.Factories.IGameObjectFactory _factory;
        IEntityFactory _entityFactory;
    }
}
