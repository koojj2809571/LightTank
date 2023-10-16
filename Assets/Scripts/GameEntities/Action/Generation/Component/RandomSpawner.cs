using Unity.Entities;
using Unity.Mathematics;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct RandomSpawner : IComponentData
    {
        public Random Ran;
    }
}