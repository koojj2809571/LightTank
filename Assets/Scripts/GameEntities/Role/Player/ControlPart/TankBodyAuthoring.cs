using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class TankBodyAuthoring : MonoBehaviour
    {

        public float moveSpeed;
        public float rotateSpeed;
        public int enemyTarget;
        
        class Baker : Baker<TankBodyAuthoring>
        {
            public override void Bake(TankBodyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ControlPart>(entity);
                AddComponent<TankBodyTag>(entity);
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