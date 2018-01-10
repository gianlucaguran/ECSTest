using System;
using UnityEngine;

namespace Svelto.ECS.Example.Survive.Components.Pickups
{
    public interface IPickUpComponent : IComponent
    {
        event System.Action<int,int> touchPickup;

        bool targetInRange { get;  set; }
    }

    public interface IAmmoPickUpComponent: IPickUpComponent
    {
        int ammoValue { get;  }
    }

    public interface IHealthPickUpComponent: IPickUpComponent
    {
        int healthValue { get; }
    }
}
