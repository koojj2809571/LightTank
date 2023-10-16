using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public readonly partial struct KeyMoveAspect : IAspect
    {
        private readonly RefRO<KeyMove> _speed;
        private readonly RefRW<LocalTransform> _trans;

        public void Move(float delta)
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");
            if (h == 0 && v == 0) return;
            var dir = math.normalize(new float3(h, 0, v));
            _trans.ValueRW.Position += dir * _speed.ValueRO.Speed * delta;
        }
    }
}