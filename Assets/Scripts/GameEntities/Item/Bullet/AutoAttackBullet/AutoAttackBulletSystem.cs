using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [UpdateAfter(typeof(AutoAimSystem))]
    public partial struct AutoAttackBulletSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AutoAttackCluster>();
            state.RequireForUpdate<Fire>();
            state.RequireForUpdate<AutoAttackBullet>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var bulletJob = new AutoAttackBulletJob
            {
                Delta = SystemAPI.Time.DeltaTime,
                Target = SystemAPI.GetComponentLookup<LocalToWorld>(),
                Trigger = SystemAPI.GetComponentLookup<TriggerDestroy>(),
            };
            state.Dependency = bulletJob.ScheduleParallel(state.Dependency);
            state.Dependency.Complete();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct AutoAttackBulletJob : IJobEntity
    {
        [ReadOnly] public float Delta;
        [ReadOnly] public ComponentLookup<LocalToWorld> Target;
        [ReadOnly] public ComponentLookup<TriggerDestroy> Trigger;

        public void Execute(Entity entity, ref AutoAttackBullet bullet, ref LocalTransform trans)
        {
            if (bullet.Status == 0) return;
            
            if (Trigger.TryGetComponent(bullet.Target, out var trigger) && trigger.IsDead == 1)
            {
                bullet.Status = 1;
                bullet.Target = bullet.Cluster;
            }
            var targetPos = Target.GetRefRO(bullet.Target).ValueRO.Position;
            if (math.abs(trans.Position.y - targetPos.y) > 0.1f)
            {
                trans.Position.y = targetPos.y;
            }

            var curPos = trans.Position;
            var distVector = targetPos - curPos;
            float velocity;
            if (bullet.Status == 1)
            {
                velocity = 10f;
                if (math.distance(targetPos, curPos) < 0.8f)
                {
                    trans = trans.RotateY(math.radians(360f) * Delta);
                }
                else
                {
                    var look = quaternion.LookRotation(distVector, math.up());
                    var deltaAngle = math.slerp(trans.Rotation, look, Delta * 60f);
                    trans.Rotation = deltaAngle;
                }
            }
            else
            {
                velocity = 20f;
                var look = quaternion.LookRotation(distVector, math.up());
                var deltaAngle = math.slerp(trans.Rotation, look, Delta * 60f);
                trans.Rotation = deltaAngle;
            }

            trans.Position = math.lerp(curPos, targetPos, velocity * Delta);
        }
    }
}