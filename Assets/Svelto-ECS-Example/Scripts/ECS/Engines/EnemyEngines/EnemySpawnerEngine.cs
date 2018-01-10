using Svelto.ECS.Example.Survive.Components.Enemy;
using Svelto.ECS.Example.Survive.Nodes.Enemies;
using Svelto.DataStructures;
using System;
using UnityEngine;
using System.Collections;
using Svelto.ECS.Example.Survive.Components.Damageable;

namespace Svelto.ECS.Example.Survive.Engines.Enemies
{
    public class EnemySpawnerEngine : SingleNodeEngine<EnemySpawningNode>, IStep<DamageInfo>
    {
        internal class EnemySpawnerData
        {
            internal GameObject enemy;
            internal float spawnQuantity;
            internal Transform[] spawnPoints;

            internal EnemySpawnerData(IEnemySpawnerComponent spawnerComponent)
            {
                enemy = spawnerComponent.enemyPrefab;
                spawnQuantity = spawnerComponent.spawnQuantity;
                spawnPoints = spawnerComponent.spawnPoints;
            }
        }

        public EnemySpawnerEngine(Factories.IGameObjectFactory factory, IEntityFactory entityFactory)
        {
            _factory = factory;
            _entityFactory = entityFactory;
            TaskRunner.Instance.Run(IntervaledTick);
        }

        IEnumerator IntervaledTick()
        {
            while (true)
            {
                yield return _waitForSecondsEnumerator;

                switch (_currentState)
                {
                    case SpawnerStates.waiting:
                        {
                            if (_waitTime < _timer)
                            {
                                _currentState = SpawnerStates.spawning;
                                _timer = 0;
                            }
                            else
                            {
                                _timer += 1.0f;
                            }
                        }
                        break;

                    case SpawnerStates.spawning:
                        {
                            SpawnEnemies();

                            //changing spawn state
                            _currentState = SpawnerStates.wavePlaying;
                        }
                        break;

                    case SpawnerStates.wavePlaying:
                        {
                            if (0 == _enemyAliveCount)
                            {
                                _currentState = SpawnerStates.waiting;
                                _timer = 0.0f;
                                _currentWave++;
                            }
                        }
                        break;

                    default:
                        break;

                }
            }
        }

        //spawn logic
        void SpawnEnemies()
        {
            for (int i = _enemiestoSpawn.Count - 1; i >= 0; --i)
            {
                var spawnData = _enemiestoSpawn[i];

                //find out how many enemies of this type we have to spawn
                int spawnCount = (int)(spawnData.spawnQuantity * _currentWave);

                //spawn enemy spawnCount times
                for (int j = spawnCount; j > 0; j--)
                {
                    // Find a random index between zero and one less than the number of spawn points.
                    int spawnPointIndex = UnityEngine.Random.Range(0, spawnData.spawnPoints.Length);

                    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                    var go = _factory.Build(spawnData.enemy);
                    _entityFactory.BuildEntity(go.GetInstanceID(), go.GetComponent<IEntityDescriptorHolder>().BuildDescriptorType());
                    var transform = go.transform;
                    var spawnInfo = spawnData.spawnPoints[spawnPointIndex];

                    transform.position = spawnInfo.position;
                    transform.rotation = spawnInfo.rotation;

                    //update counter each time
                    AddSpawnCount();

                    
                }
            }
        }

        //called when an enemy dies
        public void Step(ref DamageInfo token, Enum condition)
        {
            DecreaseSpawnCount();
        }


        void AddSpawnCount()
        {
            _enemyAliveCount++;
        }

        void DecreaseSpawnCount()
        {
            _enemyAliveCount--;
        }

        protected override void Add(EnemySpawningNode node)
        {
            var spawnerComponents = (node).spawnerComponents;

            for (int i = 0; i < spawnerComponents.Length; i++)
            {
                _enemiestoSpawn.Add(new EnemySpawnerData(spawnerComponents[i]));
            }
        }



        protected override void Remove(EnemySpawningNode node)
        { }

        FasterList<EnemySpawnerData> _enemiestoSpawn = new FasterList<EnemySpawnerData>();
        Svelto.Factories.IGameObjectFactory _factory;
        IEntityFactory _entityFactory;
        Tasks.WaitForSecondsEnumerator _waitForSecondsEnumerator = new Tasks.WaitForSecondsEnumerator(1);
        int _currentWave = 1;
        int _enemyAliveCount = 0;
        const float _waitTime = 3.0f;
        float _timer = 0.0f;
        SpawnerStates _currentState = SpawnerStates.waiting;
    }

    public enum SpawnerStates
    {
        waiting,
        spawning,
        wavePlaying,
    }
}
