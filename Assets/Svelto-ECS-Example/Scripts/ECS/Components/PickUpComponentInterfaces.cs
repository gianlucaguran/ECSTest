using System;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Components.Pickups
{
    public interface IPickUpComponent : IComponent
    {
        event System.Action<int,int> touchPickup;
    }

    public interface IAmmoPickUpComponent: IPickUpComponent
    {
        int ammoValue { get;  }
    }

    public interface IHealthPickUpComponent: IPickUpComponent
    {
        int healthValue { get; }
    }

    public interface IPickUpSpawnerComponent : IComponent
    {
        GameObject pickupPrefab { get; }
        Transform[] spawnPoints { get; }
        //bool [] spawned { get;  }
        float spawnTime         { get; }
        float spawnProbability { get; }
    }

    
}
