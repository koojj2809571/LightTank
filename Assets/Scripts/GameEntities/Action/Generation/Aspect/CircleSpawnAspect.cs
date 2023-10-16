using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public readonly partial struct CircleSpawnAspect : IAspect
    {
        private readonly RefRW<CircleSpawner> _circle;
        private readonly RefRW<LocalToWorld> _ltw;

        public float3 Center
        {
            get => _circle.ValueRO.Center;
            set => _circle.ValueRW.Center = value;
        }

        public NativeArray<float3> RandomPos(int count, Random ran)
        {
            Center = _ltw.ValueRO.Position;
            var posList = CollectionHelper.CreateNativeArray<float3>(count, Allocator.TempJob);
            for (var i = 0; i < posList.Length; i++)
            {
                var randomDir = ran.NextFloat2Direction();
                var randomRadius = ran.NextFloat(_circle.ValueRO.MinRadius, _circle.ValueRO.MaxRadius);
                var randomPos = math.normalize(randomDir) * randomRadius;
                posList[i] = new float3(randomPos.x + Center.x, 1.33f, randomPos.y + Center.z);
            }

            return posList;
        }
    }
}