using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [WithAll(typeof(EnemyTag))]
    public partial struct TriggerDestroyJob : IJobEntity
    {
        [ReadOnly] public float Dt;

        public void Execute(ref TriggerDestroy trigger, ref MaterialMeshInfo meshInfo, ref LocalTransform trans)
        {
            if (trigger.IsDead == 0) return;
            meshInfo.Mesh = 0;
            trigger.CurTime += Dt;
            if (trigger.CurTime >= trigger.DeadTime)
            {
                trans.Position = new float3(trans.Position.x, -10, trans.Position.z);
            }
        }
    }
}