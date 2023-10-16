using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct FollowMove : IComponentData, IEnableableComponent
    {
        public int MaxTargetIndex;
        public int TargetIndex;
        public float MoveSpeed;
        public float RotateSpeed;
        public float StopDistance;
        
    }
}