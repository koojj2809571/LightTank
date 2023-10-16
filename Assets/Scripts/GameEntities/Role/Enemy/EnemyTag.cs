using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct EnemyTag : IComponentData
    {
        public Entity Self;
    }
}