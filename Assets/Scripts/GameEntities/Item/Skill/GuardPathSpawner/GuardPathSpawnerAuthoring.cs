using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class GuardPathSpawnerAuthoring : MonoBehaviour
    {

        public int radius;
        
        class Baker : Baker<GuardPathSpawnerAuthoring>
        {
            public override void Bake(GuardPathSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new GuardPathSpawner
                {
                    Radius = authoring.radius
                });
            }
        }
    }
}