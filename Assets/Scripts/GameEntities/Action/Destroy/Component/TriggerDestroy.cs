using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct TriggerDestroy : IComponentData, IEnableableComponent
    {
        public int IsDead;
        public float DeadTime;
        public float CurTime;
    }
}