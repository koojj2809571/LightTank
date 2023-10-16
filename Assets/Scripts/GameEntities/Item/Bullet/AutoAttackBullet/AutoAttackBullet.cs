using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct AutoAttackBullet : IComponentData, IEnableableComponent
    {
        public Entity Target;
        public Entity Cluster;
        // 0: 直线射击; 1: 回到Tank上方聚合点，并开始旋转等待; 3: 飞向目标敌人
        public int Status;
    }
}