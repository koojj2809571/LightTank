using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct BulletSpawner : IComponentData
    {
        public int Count;
        public int Waves;
    }
}