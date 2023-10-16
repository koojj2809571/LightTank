using Unity.Entities;
using Unity.Mathematics;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct MoveWithDirection : IComponentData, IEnableableComponent
    {
        public float3 MoveDir;
        public float3 InitPos;
        public float Velocity;
    }
}