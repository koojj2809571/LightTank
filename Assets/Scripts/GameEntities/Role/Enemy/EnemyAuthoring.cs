using System;
using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class EnemyAuthoring : MonoBehaviour
    {

        public float moveSpeed;
        public float stopDistance;
        public float rotationSpeed;
        public int targetIndex;
        
        class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyTag
                {
                    Self = entity
                });
                AddComponent<TriggerDestroy>(entity);
                AddComponent(entity, new FollowMove
                {
                    MoveSpeed = authoring.moveSpeed,
                    RotateSpeed = authoring.rotationSpeed,
                    TargetIndex = authoring.targetIndex,
                    StopDistance = authoring.stopDistance
                });
            }
        }

    }
}