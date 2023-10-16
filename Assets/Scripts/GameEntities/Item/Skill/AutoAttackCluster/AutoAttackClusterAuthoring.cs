using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class AutoAttackClusterAuthoring : MonoBehaviour
    {
        public float autoAttackCd;
        
        class Baker : Baker<AutoAttackClusterAuthoring>
        {
            public override void Bake(AutoAttackClusterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new AutoAttackCluster
                {
                    AutoAttackTick = authoring.autoAttackCd
                });
            }
        }
    }
}