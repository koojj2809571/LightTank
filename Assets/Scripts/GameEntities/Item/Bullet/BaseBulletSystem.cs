using GameEntities.Job;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct BaseBulletSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Fire>();
            state.RequireForUpdate<BulletTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var ecbParallel = ecb.AsParallelWriter();

            var actionJob = new MoveWithDirectionJob
            {
                Delta = SystemAPI.Time.DeltaTime,
            };
            var lifeJob = new MaxLifeTimeJob
            {
                Delta = SystemAPI.Time.DeltaTime,
                Writer = ecbParallel,
            };
            var distJob = new MaxMoveDistanceJob
            {
                Writer = ecbParallel,
                DirMoveLookup = SystemAPI.GetComponentLookup<MoveWithDirection>(),
                FollowMoveLookup = SystemAPI.GetComponentLookup<FollowMove>(),
                AutoAttackLookup = SystemAPI.GetComponentLookup<AutoAttackBullet>()
            };
            var actionHandle = actionJob.Schedule(state.Dependency);
            var lifeHandle = lifeJob.Schedule(actionHandle);
            state.Dependency = distJob.Schedule(lifeHandle);
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