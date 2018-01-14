using UnityEngine;

namespace Svelto.ECS.Example.Survive.Components.Base
{
    public interface IAnimationComponent: IComponent
    {
        Animator animation { get; }
    }

    public interface IPositionComponent: IComponent
    {
        Vector3 position { get; }
    }

    public interface ITransformComponent: IComponent
    {
        Transform transform { get; }
    }

    public interface IRigidBodyComponent: IComponent
    {
        Rigidbody rigidbody { get; }
    }

    public interface ISpeedComponent: IComponent
    {
        float speed { get; }
    }

    public interface IDamageSoundComponent: IComponent
    {
        AudioSource audioSource { get; }
        AudioClip   death       { get; }
        AudioClip   damage      { get; }
    }

    public interface ISingleSoundComponent: IComponent
    {
        AudioSource audioSource { get; }
        AudioClip sound { get; }
    }
   
}
