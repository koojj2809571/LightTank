using Unity.Entities;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public struct UIData : IComponentData
    {
        public int CurSkillNum;
        public int KillNum;
        public float Health;
        public float CurSpeed;
        public float CurDist;
    }
}

//todo 自动瞄准有BUG