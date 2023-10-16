using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public readonly partial struct WaveSpawnAspect : IAspect
    {
        private readonly RefRW<WaveSpawner> _wave;
        private readonly RefRW<Spawner> _spawner;

        public int CurWave
        {
            get => _wave.ValueRO.Waves;
            set => _wave.ValueRW.Waves = value;
        } 
        
        public void WaveTimer(float dt)
        {
            if (_wave.ValueRO.Timer < _wave.ValueRO.Duration)
            {
                _wave.ValueRW.Timer += dt;
            }
            else
            {
                _spawner.ValueRW.IsActive = 1;
                _wave.ValueRW.Timer = 0;
                _spawner.ValueRW.SpawnerCount += _wave.ValueRO.NextWavesChangeCount;
            }
        }

    }
}