using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct MaxLifeTime : IComponentData
    {
        public float DestroyTime;
        public float Current;
    }
}