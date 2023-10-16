using Unity.Burst;
using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct EnemyDeadPSSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;

            var job = new TriggerDestroyJob { Dt = dt };
            state.Dependency = job.Schedule(state.Dependency);
            state.Dependency.Complete();
            
            foreach (var (aspect, ps) in
                     SystemAPI.Query<EnemyDeadAspect, SystemAPI.ManagedAPI.UnityEngineComponent<ParticleSystem>>())
            {
                if (aspect.Status != 1) continue;
                ps.Value.Play();
                aspect.Status = 2;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}