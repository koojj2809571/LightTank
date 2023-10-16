using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class AutoAttackBulletAuthoring : MonoBehaviour
    {
        class Baker : Baker<AutoAttackBulletAuthoring>
        {
            public override void Bake(AutoAttackBulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<AutoAttackBullet>(entity);
                AddComponent<BulletTag>(entity);
                AddComponent<TriggerDestroy>(entity);
                AddComponent<MoveWithDirection>(entity);
                AddComponent<MaxMoveDistance>(entity);
                AddComponent<MaxLifeTime>(entity);
            }
        }
    }
}