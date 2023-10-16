using Unity.Burst;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [BurstCompile]
    public partial struct FollowTargetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FollowTarget>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new FollowTargetJob();
            var handle = job.ScheduleParallel(state.Dependency);
            handle.Complete();
        }
    }
}