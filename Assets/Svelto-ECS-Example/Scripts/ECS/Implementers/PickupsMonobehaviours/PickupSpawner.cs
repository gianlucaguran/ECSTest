
using System;
using Svelto.ECS.Example.Survive.Components.Pickups;
using UnityEngine;



namespace Svelto.ECS.Example.Survive.Implementers.Pickups
{
    public class PickupSpawner : MonoBehaviour, IPickUpSpawnerComponent
    {
        //Editor variables
        public GameObject pickup;                // The object prefab to be spawned.
        public Transform[] spawnPoints;   //list of spawn positions
        public float spawnTime;         //time between each spawn
        public float spawnProbability;  //probability object will spawn (between 0 and 1 )
        //


        GameObject IPickUpSpawnerComponent.pickupPrefab { get { return pickup; } }

        Transform[] IPickUpSpawnerComponent.spawnPoints { get { return spawnPoints; } }

        float IPickUpSpawnerComponent.spawnTime { get { return spawnTime; } }

        float IPickUpSpawnerComponent.spawnProbability { get { return spawnProbability; } }



    }
}
