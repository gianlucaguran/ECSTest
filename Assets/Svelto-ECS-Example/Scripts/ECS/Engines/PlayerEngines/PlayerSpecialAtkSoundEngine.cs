
using System;
using Svelto.ECS.Example.Survive.Nodes.Player;

namespace Svelto.ECS.Example.Survive.Engines.Player.Special
{
    public class PlayerSpecialAtkSoundEngine : SingleNodeEngine<PlayerSpecialAttackSoundNode> , IStep<SpecialAttackCondition>
    {
        protected override void Add(PlayerSpecialAttackSoundNode node)
        {
            _playerNode = node;
        }

        protected override void Remove(PlayerSpecialAttackSoundNode node)
        {
            _playerNode = null;
        }

        
        public void Step(ref SpecialAttackCondition token, Enum condition)
        {
            var soundComponent = _playerNode.soundComponent;

            switch (token)
            {
                case SpecialAttackCondition.perform:
                    {
                        soundComponent.audioSource.PlayOneShot(soundComponent.performClip);
                    }
                    break;

                case SpecialAttackCondition.fail:
                    {
                        if ( ! soundComponent.audioSource.isPlaying )
                        {
                            soundComponent.audioSource.PlayOneShot(soundComponent.failClip);
                        }
                    }
                    break;

                default:
                    break;
            }

            }
        

        PlayerSpecialAttackSoundNode _playerNode;
    }
}