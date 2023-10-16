using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class GuardBulletAuthoring : MonoBehaviour
    {
        class Baker : Baker<GuardBulletAuthoring>
        {
            public override void Bake(GuardBulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<BulletTag>(entity);
                AddComponent<GuardBullet>(entity);
                AddComponent<TriggerDestroy>(entity);
                AddComponent<MoveWithDirection>(entity);
                AddComponent<MaxMoveDistance>(entity);
                AddComponent<MaxLifeTime>(entity);
                AddComponent<FollowMove>(entity);
            }
        }
    }
}