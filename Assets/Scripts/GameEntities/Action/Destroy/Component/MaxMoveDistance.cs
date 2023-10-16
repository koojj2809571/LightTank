using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct MaxMoveDistance : IComponentData, IEnableableComponent
    {
        public float DestroyDistance;
        public float Current;
        public int EndDestroy;
    }
}