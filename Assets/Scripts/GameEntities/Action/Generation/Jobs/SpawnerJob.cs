using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [BurstCompile]
    public struct SpawnerJob : IJobParallelFor
    {

        [ReadOnly] public Entity SpawnGo;
        public NativeArray<Entity> Spawners;
        public NativeArray<float3> SpawnPos;
        public EntityCommandBuffer.ParallelWriter Writer;

        public void Execute(int idx)
        {
            Spawners[idx] = Writer.Instantiate(idx, SpawnGo);
            Writer.SetComponent(idx, Spawners[idx], new LocalTransform
            {
                Position = SpawnPos[idx],
                Rotation = quaternion.identity,
                Scale = 1
            });
        }
    }
}