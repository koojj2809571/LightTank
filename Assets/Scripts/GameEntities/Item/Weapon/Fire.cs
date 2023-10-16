using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct Fire : IComponentData
    {
        public Entity LineBulletGo;
        public Entity GuardBulletGo;
        public Entity AutoAttackBulletGo;
        public float FireSpeed;
        public int FireNumEveryTime;
        public float Force;
        public float DestroyTime;
        public float DestroyDistance;
    }
}