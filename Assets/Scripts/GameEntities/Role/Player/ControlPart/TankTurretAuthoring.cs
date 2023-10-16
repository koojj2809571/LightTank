using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class TankTurretAuthoring : MonoBehaviour
    {

        public float rotateSpeed;
        public float moveSpeed;
        
        class Baker : Baker<TankTurretAuthoring>
        {
            public override void Bake(TankTurretAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ControlPart>(entity);
                AddComponent<TankTurretTag>(entity);
                AddComponent(entity, new KeyMove
                {
                    Speed = authoring.moveSpeed,
                });
                AddComponent(entity, new MouseRotation
                {
                    Speed = authoring.rotateSpeed
                });
            }
        }
    }
}