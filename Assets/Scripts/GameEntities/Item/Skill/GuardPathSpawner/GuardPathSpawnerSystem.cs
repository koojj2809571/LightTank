using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct GuardPathSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GuardPathSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var spawnerEntity = SystemAPI.GetSingletonEntity<GuardPathSpawner>();
            var guardPathSpawner = SystemAPI.GetComponentRW<GuardPathSpawner>(spawnerEntity);
            var points = SystemAPI.GetBuffer<Child>(spawnerEntity);
            guardPathSpawner.ValueRW.Total = points.Length - 1;
            var i = 0;
            foreach (var point in points)
            {
                var guardPathPoint = SystemAPI.GetComponentRW<GuardPathPoint>(point.Value);
                var followTarget = SystemAPI.GetComponentRW<FollowTarget>(point.Value);
                guardPathPoint.ValueRW.Index = i;
                followTarget.ValueRW.TargetIndex = i;
                i++;
            }
            var spawnJob = new GuardPathPointSpawnJob
            {
                Radius = guardPathSpawner.ValueRO.Radius,
                Total = points.Length,
            };
            state.Dependency = spawnJob.Schedule(state.Dependency);
            state.Dependency.Complete();
            
            state.Enabled = false;
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

    [BurstCompile]
    public partial struct GuardPathPointSpawnJob : IJobEntity
    {
        [ReadOnly] public float Radius;
        [ReadOnly] public int Total;
        public EntityCommandBuffer.ParallelWriter Writer;

        public void Execute(ref LocalTransform trans, in GuardPathPoint point)
        {
            var unitRadians = math.radians(360) / Total;
            var q = quaternion.AxisAngle(math.up(), unitRadians * point.Index);
            trans.Position = math.mul(q, new float3(0,0, Radius));
        }
    }

}