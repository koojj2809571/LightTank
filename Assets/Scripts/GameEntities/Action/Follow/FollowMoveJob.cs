using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct FollowMoveJob : IJobEntity
    {

        [ReadOnly] public float Delta;
        [ReadOnly] public int TargetIndex;
        [ReadOnly] public float3 TargetTrans;
        
        public void Execute(Entity entity, ref FollowMove move, ref LocalTransform trans, in TriggerDestroy trigger)
        {
            if (trigger.IsDead != 0) return;
            if(move.TargetIndex != TargetIndex) return;
            var targetPos = TargetTrans;
            var curPos = new float3(trans.Position.x, targetPos.y, trans.Position.z);
            var distVector = targetPos - curPos;
            if (move.StopDistance != 0 && math.distance(targetPos, curPos) < move.StopDistance)
            {
                if (move.MaxTargetIndex != 0)
                {
                    move.TargetIndex = move.TargetIndex >= move.MaxTargetIndex ? 0 : move.TargetIndex + 1;
                }
                return;
            }
            var look = quaternion.LookRotation(distVector,math.up());
            var deltaAngle = math.slerp(trans.Rotation, look, Delta * move.RotateSpeed);
            trans.Rotation = deltaAngle;
            trans.Position = math.lerp(curPos, targetPos, move.MoveSpeed * Delta);
        }
    }
}