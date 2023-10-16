using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct WaveSpawner : IComponentData
    {
        public int Waves;
        public int NextWavesChangeCount;
        public float Duration;
        public float Timer;
    }
}