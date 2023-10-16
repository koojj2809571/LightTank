using Unity.Burst;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct FollowMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FollowTarget>();
            state.RequireForUpdate<FollowMove>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
            var delta = SystemAPI.Time.DeltaTime;
            foreach (var target in SystemAPI.Query<RefRO<FollowTarget>>())
            {
                var job = new FollowMoveJob
                {
                    Delta = delta,
                    TargetIndex = target.ValueRO.TargetIndex,
                    TargetTrans = target.ValueRO.CurTransform
                };
                var handle = job.ScheduleParallel(state.Dependency);
                handle.Complete();
            }
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}