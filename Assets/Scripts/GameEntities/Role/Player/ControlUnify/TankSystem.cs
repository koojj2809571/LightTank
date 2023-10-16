using Unity.Burst;
using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct TankSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ControlUnify>();
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var delta = SystemAPI.Time.DeltaTime;
            var player = SystemAPI.GetSingletonEntity<PlayerTag>();
            var rotationAspect = SystemAPI.GetAspect<KeyRotationAspect>(player);
            rotationAspect.Rotate(delta);
            foreach (var keyMoveAspect in SystemAPI.Query<KeyMoveAspect>())
            {
                keyMoveAspect.Move(delta);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}