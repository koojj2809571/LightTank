using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct TankTurretSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<TankTurretTag>();
            state.RequireForUpdate<ControlPart>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var delta = SystemAPI.Time.DeltaTime;
            var turret = SystemAPI.GetSingletonEntity<TankTurretTag>();
            var keyMoveAspect = SystemAPI.GetAspect<KeyMoveAspect>(turret);
            var mouseRotationAspect = SystemAPI.GetAspect<MouseRotationAspect>(turret);
            var collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;
            keyMoveAspect.Move(delta);
            mouseRotationAspect.Rotate(delta, collisionWorld);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}