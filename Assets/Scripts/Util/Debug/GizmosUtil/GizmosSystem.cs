using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct GizmosSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MyGizmos>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var gizmos = SystemAPI.GetSingleton<MyGizmos>();
            var entity = SystemAPI.GetSingletonEntity<MyGizmos>();
            var transform = state.EntityManager.GetComponentData<LocalTransform>(entity);
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var point = ecb.Instantiate(gizmos.GizmosPointIcon);
            ecb.AddComponent(point, transform);
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}