using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class GuardPathPointAuthoring : MonoBehaviour
    {
        class Baker : Baker<GuardPathPointAuthoring>
        {
            public override void Bake(GuardPathPointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<FollowTarget>(entity);
                AddComponent<GuardPathPoint>(entity);
            }
        }
    }
}