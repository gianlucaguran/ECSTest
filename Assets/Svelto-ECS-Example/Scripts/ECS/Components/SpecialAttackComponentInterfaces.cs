
namespace Svelto.ECS.Example.Survive.Components.Player
{
    public interface ISpecialAttackComponent : IComponent
    {
        float cooldownTimer { get; set; }
        //read only properties, configured by inspector
        float cooldown { get;  }
        float power { get;   }
        float distance { get;  }
    }
}
