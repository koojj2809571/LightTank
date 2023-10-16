using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class UIDataAuthoring : MonoBehaviour
    {
        class Baker : Baker<UIDataAuthoring>
        {
            public override void Bake(UIDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new UIData
                {
                    Health = 1,
                    KillNum = 0,
                    CurSkillNum = 1,
                    CurDist = 0,
                    CurSpeed = 1,
                });
            }
        }
    }
}