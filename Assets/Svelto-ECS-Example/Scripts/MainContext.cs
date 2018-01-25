using Svelto.ECS.Example.Survive.Engines.Enemies;
using Svelto.ECS.Example.Survive.Engines.Health;
using Svelto.ECS.Example.Survive.Engines.HUD;
using Svelto.ECS.Example.Survive.Engines.Player;
using Svelto.ECS.Example.Survive.Engines.Player.Gun;
using Svelto.ECS.Example.Survive.Engines.Sound.Damage;
using Svelto.ECS.Example.Survive.Engines.Pickups;
using Svelto.ECS.Example.Survive.Observables.Enemies;
using Svelto.ECS.Example.Survive.Observers.HUD;
using Svelto.Context;
using UnityEngine;
using Steps = System.Collections.Generic.Dictionary<Svelto.ECS.IEngine, System.Collections.Generic.Dictionary<System.Enum, Svelto.ECS.IStep[]>>;
using System.Collections.Generic;
using Svelto.ECS.NodeSchedulers;

//Main is the Application Composition Root.
//Composition Root is the place where the framework can be initialised.
namespace Svelto.ECS.Example.Survive
{
    public class Main : ICompositionRoot
    {
        public Main()
        {
            SetupEnginesAndComponents();
        }

        void SetupEnginesAndComponents()
        {
            _entityFactory = _enginesRoot = new EnginesRoot(new UnitySumbmissionNodeScheduler());

            GameObjectFactory factory = new GameObjectFactory();

            var enemyKilledObservable = new EnemyKilledObservable();
            var scoreOnEnemyKilledObserver = new ScoreOnEnemyKilledObserver(enemyKilledObservable);

            Sequencer playerDamageSequence = new Sequencer();
            Sequencer enemyDamageSequence = new Sequencer();
            Sequencer playerHealSequence = new Sequencer();
            Sequencer ammoRechargeSequence = new Sequencer();

            var enemyAnimationEngine = new EnemyAnimationEngine();
            var playerHealthEngine = new HealthEngine(playerDamageSequence, playerHealSequence );
            var enemyHealthEngine = new HealthEngine(enemyDamageSequence);
            var hudEngine = new HUDEngine();
            var damageSoundEngine = new DamageSoundEngine();
            var playerShootingEngine = new PlayerGunShootingEngine(enemyKilledObservable, enemyDamageSequence);
            var playerMovementEngine = new PlayerMovementEngine();
            var playerAnimationEngine = new PlayerAnimationEngine();
            var enemyAttackEngine = new EnemyAttackEngine(playerDamageSequence);
            var enemyMovementEngine = new EnemyMovementEngine();
            var enemySpawnerEngine = new EnemySpawnerEngine(factory, _entityFactory);
            var healingPickupEngine = new HealthPickupEngine(playerHealSequence);
            var ammoPickupEngine = new AmmoPickupEngine(ammoRechargeSequence);
            var pickupSpawnerEngine = new PickupSpawnerEngine(factory, _entityFactory);
            var pickupSoundEngine = new PickupSoundEngine();
            

            playerDamageSequence.SetSequence(
                new Steps() //sequence of steps
                {
                    { //first step
                        enemyAttackEngine, //this step can be triggered only by this engine through the Next function
                        new Dictionary<System.Enum, IStep[]>() //this step can lead only to one branch
                        {
                            {  Condition.always, new [] { playerHealthEngine }  }, //these engines will be called when the Next function is called with the Condition.always set
                        }
                    },
                    { //second step
                        playerHealthEngine, //this step can be triggered only by this engine through the Next function
                        new Dictionary<System.Enum, IStep[]>() //this step can branch in two paths
                        {
                            {  DamageCondition.damage, new IStep[] { hudEngine, damageSoundEngine }  }, //these engines will be called when the Next function is called with the DamageCondition.damage set
                            {  DamageCondition.dead, new IStep[] { hudEngine, damageSoundEngine, playerMovementEngine, playerAnimationEngine, enemyAnimationEngine }  }, //these engines will be called when the Next function is called with the DamageCondition.dead set
                        }
                    }
                }
            );

            enemyDamageSequence.SetSequence(
                new Steps()
                {
                    {
                        playerShootingEngine,
                        new Dictionary<System.Enum, IStep[]>()
                        {
                            {  Condition.always, new [] { enemyHealthEngine }  },
                        }
                    },
                    {
                        enemyHealthEngine,
                        new Dictionary<System.Enum, IStep[]>()
                        {
                            {  DamageCondition.damage, new IStep[] { enemyAnimationEngine }  },
                            {  DamageCondition.dead, new IStep[] { enemyMovementEngine, enemyAnimationEngine, playerShootingEngine, enemySpawnerEngine }  },
                        }
                    }
                }
            );

            //healing sequence 
            playerHealSequence.SetSequence(
                new Steps()
                {
                    {
                        healingPickupEngine,
                        new Dictionary<System.Enum, IStep[]>()
                        {
                            {DamageCondition.heal, new IStep[]{ playerHealthEngine } },
                        }
                    },

                     { //second step
                        playerHealthEngine, 
                        new Dictionary<System.Enum, IStep[]>()  
                        {
                            {  DamageCondition.heal, new IStep[] { hudEngine/*, pickupSoundEngine*/ }  }, 
                        }
                    }
                }
             );

            ////Sequence for ammo recharge
            //ammoRechargeSequence.SetSequence(
            //    new Steps()
            //    {
            //        //{
            //        //    ammoPickupEngine,
            //        //    new Dictionary<System.Enum, IStep[]>()
            //        //    {
            //        //        {Condition.always, new IStep[]{  } },
            //        //    }
            //        //},

            //    }
            //    );

            AddEngine(playerMovementEngine);
            AddEngine(playerAnimationEngine);
            AddEngine(playerShootingEngine);
            AddEngine(playerHealthEngine);
            AddEngine(new PlayerGunShootingFXsEngine());

            AddEngine(enemySpawnerEngine);
            AddEngine(enemyAttackEngine);
            AddEngine(enemyMovementEngine);
            AddEngine(enemyAnimationEngine);
            AddEngine(enemyHealthEngine);

            AddEngine(damageSoundEngine);
            AddEngine(hudEngine);
            AddEngine(new ScoreEngine(scoreOnEnemyKilledObserver));
            AddEngine(healingPickupEngine);
            AddEngine(ammoPickupEngine);
            AddEngine(pickupSpawnerEngine);
            AddEngine(pickupSoundEngine);
        }

        void AddEngine(IEngine engine)
        {
            _enginesRoot.AddEngine(engine);
        }

        void ICompositionRoot.OnContextCreated(UnityContext contextHolder)
        {
            IEntityDescriptorHolder[] entities = contextHolder.GetComponentsInChildren<IEntityDescriptorHolder>();

            for (int i = 0; i < entities.Length; i++)
            {
                
                _entityFactory.BuildEntity((entities[i] as MonoBehaviour).gameObject.GetInstanceID(), entities[i].BuildDescriptorType());
            }
        }

        void ICompositionRoot.OnContextInitialized()
        { }

        void ICompositionRoot.OnContextDestroyed()
        { }

        EnginesRoot _enginesRoot;
        IEntityFactory _entityFactory;

    }

    //A GameObject containing UnityContext must be present in the scene
    //All the monobehaviours present in the scene statically that need
    //to notify the Context, must belong to GameObjects children of UnityContext.

    public class MainContext : UnityContext<Main>
    { }

}