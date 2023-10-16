using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class AroundPlayerSpawnerAuthoring : MonoBehaviour
    {
        public GameObject enemyGo;
        public int firstWaveCount;
        public int waves;
        public float minDistance;
        public float maxDistance;
        public float waveDuration;
        public int perWaveAddCount;
        public uint randomSeed;
        
        class Baker : Baker<AroundPlayerSpawnerAuthoring>
        {
            public override void Bake(AroundPlayerSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<AroundPlayerSpawnerTag>(entity);
                AddComponent(entity, new Spawner
                {
                    IsActive = 0,
                    SpawnerCount = authoring.firstWaveCount,
                    SpawnerGo = GetEntity(authoring.enemyGo, TransformUsageFlags.Dynamic)
                });
                AddComponent(entity, new RandomSpawner
                {
                    Ran = new Random(authoring.randomSeed)
                });
                AddComponent(entity, new WaveSpawner
                {
                    Waves = authoring.waves,
                    Duration = authoring.waveDuration,
                    NextWavesChangeCount = authoring.perWaveAddCount,
                    Timer = 0
                });
                AddComponent(entity, new CircleSpawner
                {
                    Center = float3.zero,
                    MaxRadius = authoring.maxDistance,
                    MinRadius = authoring.minDistance
                });
                
            }
        }
    }
}