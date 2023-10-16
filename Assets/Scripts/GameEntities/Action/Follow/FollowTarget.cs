using Unity.Entities;
using Unity.Mathematics;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct FollowTarget : IComponentData
    {
        public int TargetIndex;
        public float3 CurTransform;
    }
}