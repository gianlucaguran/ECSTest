using Svelto.ECS.Example.Survive.Components.Enemy;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Implementers.Enemies
{
    public class EnemySpawner : MonoBehaviour, IEnemySpawnerComponent
    {
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnQuantity = 1f;            // How many of this enemy have to spawn ?  //set on inspector for each type of enemy
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

        GameObject IEnemySpawnerComponent.enemyPrefab { get { return enemy; } }
        float IEnemySpawnerComponent.spawnQuantity { get { return spawnQuantity; } }
        Transform[] IEnemySpawnerComponent.spawnPoints { get { return spawnPoints; } }
    }
}
