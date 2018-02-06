using System;
using Svelto.ECS.Example.Survive.Components.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Survive.Implementers.HUD
{
    public class AmmoManager : MonoBehaviour, IAmmoCountComponent
    {
        int IAmmoCountComponent.ammo  {  get   { return _ammo;  }  set  { _ammo = value; _text.text = "Ammo: " + value; } }
        
        void Awake()
        {
            // Set up the reference.
            _text = GetComponent<Text>();

            // Reset the score.
            _ammo = 0;
        }

        int _ammo;
        Text _text;
    }
}
