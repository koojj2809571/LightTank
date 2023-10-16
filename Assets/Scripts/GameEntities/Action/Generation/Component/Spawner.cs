using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct Spawner : IComponentData
    {
        public Entity SpawnerGo;
        public int SpawnerCount;
        public int IsActive;
    }
}