using Unity.Entities;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct FollowTargetJob : IJobEntity
    {
        public void Execute(Entity entity, ref FollowTarget target, in LocalToWorld trans)
        {
            target.CurTransform = trans.Position;
        }
    }
}