using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct TurretSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ControlUnify>();
            state.RequireForUpdate<TankTurretTag>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var delta = SystemAPI.Time.DeltaTime;
            var turret = SystemAPI.GetSingletonEntity<TankTurretTag>();
            var tank = SystemAPI.GetComponentRO<Parent>(turret).ValueRO.Value;
            var tankTrans = SystemAPI.GetComponentRO<LocalToWorld>(tank);
            var mouseRotationAspect = SystemAPI.GetAspect<MouseRotationAspect>(turret);
            var collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;
            mouseRotationAspect.RotateLocal(delta, collisionWorld, tankTrans.ValueRO);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}