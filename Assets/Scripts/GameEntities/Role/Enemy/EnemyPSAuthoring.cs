using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class EnemyPSAuthoring : MonoBehaviour
    {
        class Baker: Baker<EnemyPSAuthoring>
        {
            public override void Bake(EnemyPSAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyDeadPS { PsStatus = 0 });
            }
        }
    }
}