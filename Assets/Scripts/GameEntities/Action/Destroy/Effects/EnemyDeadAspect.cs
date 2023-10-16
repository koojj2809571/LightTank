using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public readonly partial struct EnemyDeadAspect : IAspect
    {
        public readonly RefRW<EnemyDeadPS> PsTag;

        public int Status
        {
            get => PsTag.ValueRO.PsStatus;
            set => PsTag.ValueRW.PsStatus = value;
        }
    }
}