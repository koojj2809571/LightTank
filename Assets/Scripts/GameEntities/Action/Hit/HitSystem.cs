using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct HitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<UIData>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var simulation = SystemAPI.GetSingleton<SimulationSingleton>();
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var triggerJob = new BulletHitTriggerJob
            {
                TriggerLookup = SystemAPI.GetComponentLookup<TriggerDestroy>(),
                EnemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(),
                BulletLookup = SystemAPI.GetComponentLookup<BulletTag>(),
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(),
                Ecb = ecb,
                ChildLookup = SystemAPI.GetBufferLookup<Child>(),
                DeadPSLookup = SystemAPI.GetComponentLookup<EnemyDeadPS>(),
                UILookup = SystemAPI.GetComponentLookup<UIData>(),
                UiEntity = SystemAPI.GetSingletonEntity<UIData>()
            };
            state.Dependency = triggerJob.Schedule(simulation, state.Dependency);
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}