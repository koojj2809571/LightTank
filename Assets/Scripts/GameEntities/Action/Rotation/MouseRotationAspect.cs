using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public readonly partial struct MouseRotationAspect : IAspect
    {
        private readonly RefRO<MouseRotation> _speed;
        private readonly RefRW<LocalTransform> _trans;

        private void GetRayInput(out RaycastInput raycastInput)
        {
            if (Camera.main == null)
            {
                raycastInput = new RaycastInput
                {
                    Start = float3.zero,
                    End = float3.zero,
                    Filter = CollisionFilter.Zero
                };
                return;
            }
            var main = Camera.main;
            var clickPos = Input.mousePosition;
            
            var ray = main.ScreenPointToRay(clickPos);

            raycastInput = new RaycastInput
            {
                Start = ray.origin,
                End = ray.origin + ray.direction * 100f,
                Filter = CollisionFilter.Default
            };
        }
        
        public void Rotate(float delta, CollisionWorld world)
        {
            GetRayInput(out var rayInput);
            if (!world.CastRay(rayInput!, out var hit)) return;
            var cur = _trans.ValueRO.Rotation;
            var pHit = hit.Position;
            var hitPoint = new float3(pHit.x, 0, pHit.z);
            var curPoint = new float3(_trans.ValueRO.Position.x, 0, _trans.ValueRO.Position.z);
            var target = quaternion.LookRotationSafe(hitPoint - curPoint, math.up());
            _trans.ValueRW.Rotation = math.slerp(cur, target, delta * _speed.ValueRO.Speed);
        }
        
        public void RotateLocal(float delta, CollisionWorld world, LocalToWorld parent)
        {
            GetRayInput(out var rayInput);
            if (!world.CastRay(rayInput!, out var hit)) return;
            var cur = _trans.ValueRO.Rotation;
            var pHit = hit.Position;
            var hitPoint = math.inverse(parent.Value).TransformPoint(new float3(pHit.x, 0, pHit.z));
            var target = quaternion.LookRotationSafe(hitPoint, math.up());
            _trans.ValueRW.Rotation = math.slerp(cur, target, delta * _speed.ValueRO.Speed);
        }

    }
}