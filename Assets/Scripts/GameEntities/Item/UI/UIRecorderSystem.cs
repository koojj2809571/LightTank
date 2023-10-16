using Manager;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct UIRecorderSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<UIData>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var uiData = SystemAPI.GetSingletonRW<UIData>();
            GameUIManager.Instance.killNum = uiData.ValueRO.KillNum;
            GameUIManager.Instance.health = math.max(uiData.ValueRO.Health, 0);
            uiData.ValueRW.CurSkillNum = GameUIManager.Instance.skillNum;
            uiData.ValueRW.CurSpeed = GameUIManager.Instance.propertyControlList[0].value;
            uiData.ValueRW.CurDist = GameUIManager.Instance.propertyControlList[1].value;;
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}