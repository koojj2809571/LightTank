using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [UpdateAfter(typeof(BaseBulletSystem))]
    public partial struct AutoAimSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AutoAttackCluster>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var clusterEntity = SystemAPI.GetSingletonEntity<AutoAttackCluster>();
            var autoAttackCluster = SystemAPI.GetComponentRW<AutoAttackCluster>(clusterEntity);
            autoAttackCluster.ValueRW.Timer += SystemAPI.Time.DeltaTime;
            
            var clusterPos = SystemAPI.GetComponentRO<LocalToWorld>(clusterEntity);
            foreach (var (enemy, ltw, trigger) in SystemAPI
                         .Query<RefRW<EnemyTag>, RefRO<LocalToWorld>, RefRO<TriggerDestroy>>())
            {
                if (trigger.ValueRO.IsDead == 1) continue;
                var enemyPos = new float2(ltw.ValueRO.Position.xz);
                var cluster = new float2(clusterPos.ValueRO.Position.xz);
                if (math.distance(enemyPos, cluster) >= 10) continue;
                foreach (var bullet in SystemAPI.Query<RefRW<AutoAttackBullet>>())
                {
                    if (autoAttackCluster.ValueRO.Timer < autoAttackCluster.ValueRO.AutoAttackTick)
                    {
                        return;
                    }
                    autoAttackCluster.ValueRW.Timer = 0;
                    if (bullet.ValueRO.Status != 1) continue;
                    bullet.ValueRW.Status = 2;
                    bullet.ValueRW.Target = enemy.ValueRO.Self;
                    break;
                }
            }
            
        }
        
        

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }

}