using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public readonly partial struct KeyRotationAspect : IAspect
    {
        private readonly RefRO<KeyRotation> _speed;
        private readonly RefRW<LocalTransform> _trans;
        
        public bool Rotate(float delta)
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");
            if (h == 0 && v == 0) return false;
            var dir = new float3(h, 0, v);
            var cur = _trans.ValueRO.Rotation;
            var target = quaternion.LookRotationSafe(dir, math.up());
            var deltaAngle = math.slerp(cur, target, delta * _speed.ValueRO.Speed);
            _trans.ValueRW.Rotation = deltaAngle;
            return true;
        }
    }
}