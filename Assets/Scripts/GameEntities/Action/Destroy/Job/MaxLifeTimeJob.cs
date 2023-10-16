using Unity.Collections;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities.Job
{
    public partial struct MaxLifeTimeJob : IJobEntity
    {

        [ReadOnly] public float Delta;
        public EntityCommandBuffer.ParallelWriter Writer;
        
        public void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity , ref MaxLifeTime destroy)
        {
            if (destroy.DestroyTime <= 0) return;
            destroy.Current += Delta;
            if (destroy.Current < destroy.DestroyTime) return;
            Writer.DestroyEntity(chunkIndex,entity);
        }
    }
}