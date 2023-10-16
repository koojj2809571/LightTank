using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public int countPerWave;
        public int waves;
        public float minDistance;
        public float maxDistance; 
        
        class Baker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                
            }
        }
    }
}