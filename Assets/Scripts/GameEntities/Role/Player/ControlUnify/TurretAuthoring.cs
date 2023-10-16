using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class TurretAuthoring : MonoBehaviour
    {

        public float rotateSpeed;
        
        class Baker : Baker<TurretAuthoring>
        {
            public override void Bake(TurretAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ControlUnify>(entity);
                AddComponent<TankTurretTag>(entity);
                AddComponent(entity, new MouseRotation
                {
                    Speed = authoring.rotateSpeed
                });
            }
        }
    }
}