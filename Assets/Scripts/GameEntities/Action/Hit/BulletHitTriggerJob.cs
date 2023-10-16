using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [BurstCompile]
    public struct BulletHitTriggerJob : ITriggerEventsJob
    {
        
        public ComponentLookup<TriggerDestroy> TriggerLookup;
        public ComponentLookup<EnemyTag> EnemyLookup;
        public ComponentLookup<BulletTag> BulletLookup;
        public ComponentLookup<PlayerTag> PlayerLookup;
        public ComponentLookup<EnemyDeadPS> DeadPSLookup;
        public BufferLookup<Child> ChildLookup;
        public EntityCommandBuffer Ecb;
        public Entity UiEntity;
        public ComponentLookup<UIData> UILookup;

        public void Execute(TriggerEvent triggerEvent)
        {
            
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;

            var aDestroy = TriggerLookup.HasComponent(entityA);
            var bDestroy = TriggerLookup.HasComponent(entityB);

            if (!aDestroy || !bDestroy) return;

            var aIsEnemy = EnemyLookup.HasComponent(entityA);
            var bIsEnemy = EnemyLookup.HasComponent(entityB);
            
            var aIsPlayer = PlayerLookup.HasComponent(entityA);
            var bIsPlayer = PlayerLookup.HasComponent(entityB);

            var children = new DynamicBuffer<Child>();
            var triggerDestroy = new RefRW<TriggerDestroy>();
            var uiData = UILookup.GetRefRW(UiEntity);
            if (aIsEnemy)
            {
                triggerDestroy = TriggerLookup.GetRefRW(entityA);
                children = ChildLookup[entityA];
                
                if (!bIsPlayer && triggerDestroy.ValueRO.IsDead == 0)
                {
                    TriggerLookup.SetComponentEnabled(entityB, false);
                    Ecb.DestroyEntity(entityB);
                }
            }
            
            if (bIsEnemy)
            {
                triggerDestroy = TriggerLookup.GetRefRW(entityB);
                children = ChildLookup[entityB];
                
                if (!aIsPlayer && triggerDestroy.ValueRO.IsDead == 0)
                {
                    TriggerLookup.SetComponentEnabled(entityA, false);
                    Ecb.DestroyEntity(entityA);
                }
            }
            triggerDestroy.ValueRW.IsDead = 1;
            triggerDestroy.ValueRW.DeadTime = 1.5f;
            
            var enemyDeadPS = DeadPSLookup.GetRefRW(children[0].Value);
            if (enemyDeadPS.ValueRO.PsStatus == 0)
            {
                if (aIsPlayer && bIsEnemy || aIsEnemy && bIsPlayer)
                {
                    uiData.ValueRW.Health -= 0.01f;
                }
                uiData.ValueRW.KillNum += 1;
            }
            enemyDeadPS.ValueRW.PsStatus = 1;
        }
    }
}