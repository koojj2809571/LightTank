using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities.Job
{
    public partial struct MaxMoveDistanceJob : IJobEntity
    {

        public ComponentLookup<MoveWithDirection> DirMoveLookup;
        public ComponentLookup<FollowMove> FollowMoveLookup;
        public ComponentLookup<AutoAttackBullet> AutoAttackLookup;
        public EntityCommandBuffer.ParallelWriter Writer;
        
        public void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity, in LocalTransform trans, ref MaxMoveDistance destroy)
        {
            if (destroy.DestroyDistance <= 0) return;
            var move = DirMoveLookup[entity];
            destroy.Current = math.length(trans.Position - move.InitPos);
            if (destroy.Current < destroy.DestroyDistance) return;
            if (destroy.EndDestroy == 1)
            {
                Writer.DestroyEntity(chunkIndex,entity);
            }
            else
            {
                DirMoveLookup.SetComponentEnabled(entity, false);
                if (FollowMoveLookup.HasComponent(entity))
                {
                    FollowMoveLookup.SetComponentEnabled(entity, true);
                }
                if (AutoAttackLookup.HasComponent(entity))
                {
                    AutoAttackLookup.GetRefRW(entity).ValueRW.Status = 1;
                }
            }
        }
    }
}