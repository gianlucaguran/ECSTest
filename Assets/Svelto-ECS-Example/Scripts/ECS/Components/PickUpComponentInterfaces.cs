using Svelto.ECS.Example.Survive.Components.Damageable;
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

    public interface IPickUpSoundReaction : IComponent
    {
        AudioSource audioSource { get; } 
    }
    
    public interface IAmmoSoundReaction: IPickUpSoundReaction
    {
        AudioClip sound { get; }
    }

    public interface IHealthSoundReaction : IPickUpSoundReaction
    {
        AudioClip sound { get; }
    }

    public interface IPickupInfo
    {
        PickupType  type {get;}
        int entity { get; }
        int pickup { get; }
        int value { get; }
    }

    public struct AmmoPickupInfo: IPickupInfo
    {
        public PickupType type { get; private set; }
        public int entity { get; private set; }
        public int pickup { get; private set; }
        public int value { get; private set; }

        public AmmoPickupInfo(int entity, int pickup, int value)
        {
            type = PickupType.Ammo;
            this.entity = entity;
            this.pickup = pickup;
            this.value = value;
        }
    }

    public struct HealthPickupInfo: IPickupInfo
    {
        public PickupType type { get; private set; }
        public int entity { get; private set; }
        public int pickup { get; private set; }
        public int value { get; private set; }

        public HealthPickupInfo(int entity, int pickup, int value)
        {
            type = PickupType.Health;
            this.entity = entity;
            this.pickup = pickup;
            this.value = value;
        }
         
    }

    public enum PickupType
    {
        Ammo,
        Health,
    }
}
