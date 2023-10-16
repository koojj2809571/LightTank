using Unity.Entities;
using Unity.Mathematics;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct CircleSpawner : IComponentData
    {
        public float MinRadius;
        public float MaxRadius;
        public float3 Center;
    }
}