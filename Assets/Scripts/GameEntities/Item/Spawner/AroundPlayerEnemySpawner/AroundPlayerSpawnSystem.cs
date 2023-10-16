using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct AroundPlayerSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AroundPlayerSpawnerTag>();
            state.RequireForUpdate<Spawner>();
            state.RequireForUpdate<CircleSpawner>();
            state.RequireForUpdate<WaveSpawner>();
            state.RequireForUpdate<RandomSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entity = SystemAPI.GetSingletonEntity<AroundPlayerSpawnerTag>();
            var spawner = SystemAPI.GetComponentRW<Spawner>(entity);
            var wave = SystemAPI.GetAspect<WaveSpawnAspect>(entity);
            if (wave.CurWave == 0)
            {
                state.Enabled = false;
                return;
            }
            var dt = SystemAPI.Time.DeltaTime;
            wave.WaveTimer(dt);
            
            if (spawner.ValueRO.IsActive == 0) return;

            var total = spawner.ValueRO.SpawnerCount;

            var circle = SystemAPI.GetAspect<CircleSpawnAspect>(entity);
            var randomSpawner = SystemAPI.GetComponentRW<RandomSpawner>(entity);


            var enemies = CollectionHelper.CreateNativeArray<Entity>(total, Allocator.TempJob);
            var posList = circle.RandomPos(total, randomSpawner.ValueRW.Ran);
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var writer = ecb.AsParallelWriter();
            var job = new SpawnerJob
            {
                SpawnGo = spawner.ValueRO.SpawnerGo,
                Spawners = enemies,
                SpawnPos = posList,
                Writer = writer
            };
            state.Dependency = job.Schedule(enemies.Length, 1, state.Dependency);
            state.Dependency.Complete();
            spawner.ValueRW.IsActive = 0;
            wave.CurWave -= 1;
            ecb.Playback(state.EntityManager);
            
            posList.Dispose();
            enemies.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}