using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class LineBulletAuthoring : MonoBehaviour
    {
        class Baker : Baker<LineBulletAuthoring>
        {
            public override void Bake(LineBulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<BulletTag>(entity);
                AddComponent<LineBullet>(entity);
                AddComponent<TriggerDestroy>(entity);
                AddComponent<MoveWithDirection>(entity);
                AddComponent<MaxLifeTime>(entity);
                AddComponent<MaxMoveDistance>(entity);
            }
        }
    }
}