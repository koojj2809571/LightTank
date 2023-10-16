using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct CameraSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<FollowTarget>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var camera = Camera.main;
            if (camera == null) return;
            
            foreach (var (target, ltw) in SystemAPI
                         .Query<RefRO<FollowTarget>, RefRW<LocalTransform>>()
                         .WithAll<PlayerTag>())
            {
                var cameraTransform = camera.transform;
                var transform = target.ValueRO.CurTransform;
                cameraTransform.position = new Vector3(transform.x + 2, cameraTransform.position.y, transform.z - 5);
            }
            
        }
    }
}