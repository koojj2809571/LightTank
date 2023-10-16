using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct MoveWithDirectionJob : IJobEntity
    {
        [ReadOnly] public float Delta;
        public void Execute(ref LocalTransform trans, ref MoveWithDirection move)
        {
            trans.Position += move.MoveDir * Delta * move.Velocity;
        }
    }
}