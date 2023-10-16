using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct GuardPathSpawner : IComponentData
    {
        public int Total;
        public float Radius;
    }
}