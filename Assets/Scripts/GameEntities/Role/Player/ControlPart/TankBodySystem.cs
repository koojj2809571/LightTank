using Unity.Burst;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    [DisableAutoCreation]
    public partial struct TankBodySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ControlPart>();
            state.RequireForUpdate<TankBodyTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var delta = SystemAPI.Time.DeltaTime;
            var body = SystemAPI.GetSingletonEntity<TankBodyTag>();
            var move = SystemAPI.GetAspect<KeyMoveAspect>(body);
            var rotation = SystemAPI.GetAspect<KeyRotationAspect>(body);
            move.Move(delta);
            var isRotation = rotation.Rotate(delta);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}