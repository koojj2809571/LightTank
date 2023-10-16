using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{

    public struct MyGizmos : IComponentData
    {
        public Entity GizmosPointIcon;
        public Entity GizmosLineCube;
        public float PointRadius;
    }
    
    public class GizmosAuthoring : MonoBehaviour
    {

        public GameObject pointGo;
        public GameObject lineGo;
        public float radius;

        class Baker: Baker<GizmosAuthoring>
        {
            public override void Bake(GizmosAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MyGizmos
                {
                    GizmosPointIcon = GetEntity(authoring.pointGo, TransformUsageFlags.Dynamic),
                    GizmosLineCube = GetEntity(authoring.lineGo, TransformUsageFlags.Dynamic),
                    PointRadius = authoring.radius
                });
            }
        }
    }
}