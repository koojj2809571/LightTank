using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class BulletSpawnerAuthoring : MonoBehaviour
    {
        public int count;
        public int waves;

        class Baker: Baker<BulletSpawnerAuthoring>
        {
            public override void Bake(BulletSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletSpawner
                {
                    Count = authoring.count,
                    Waves = authoring.waves
                });
            }
        }
    }
}