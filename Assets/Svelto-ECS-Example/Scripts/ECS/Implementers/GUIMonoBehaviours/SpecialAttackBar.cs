using Svelto.ECS.Example.Survive.Components.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Survive.Implementers.HUD
{
    public class SpecialAttackBar : MonoBehaviour , ISpecialBarComponent
    {
        Slider ISpecialBarComponent.bar { get { return _slider; } }



        GameObject ISpecialBarComponent.readyIcon { get { return _icon; } }



        void Awake()
        {
            _slider = this.GetComponentInChildren<Slider>();
            _icon = transform.GetChild(1).gameObject;
            Debug.Log(_icon);
        }

        Slider _slider;
        GameObject _icon;
    }
}
