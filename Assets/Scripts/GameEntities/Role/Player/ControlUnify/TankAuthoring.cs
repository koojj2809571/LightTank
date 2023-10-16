using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class TankAuthoring : MonoBehaviour
    {

        public float moveSpeed;
        public float rotateSpeed;
        public int enemyTarget;
        
        class Baker : Baker<TankAuthoring>
        {
            public override void Bake(TankAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ControlUnify>(entity);
                AddComponent<PlayerTag>(entity);
                AddComponent<TriggerDestroy>(entity);
                AddComponent(entity, new FollowTarget
                {
                    TargetIndex = authoring.enemyTarget
                });
                AddComponent(entity, new KeyMove
                {
                    Speed = authoring.moveSpeed,
                });
                AddComponent(entity, new KeyRotation
                {
                    Speed = authoring.rotateSpeed
                });
            }
        }
    }
}