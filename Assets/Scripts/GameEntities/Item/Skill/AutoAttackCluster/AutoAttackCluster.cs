using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct AutoAttackCluster : IComponentData
    {
        public float Timer;
        public float AutoAttackTick;
    }
}