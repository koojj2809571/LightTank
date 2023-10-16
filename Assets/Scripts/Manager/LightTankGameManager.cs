using UnityEngine;
using Util.Base;


namespace Manager
{
    /// 武器系统
    /// --武器属性（可以相互影响，例如增大体积会减少飞行速度及弹夹数量）
    /// ----子弹飞行速度
    /// ----子弹体积
    /// ----弹夹数量
    /// ----射击速度
    /// ----子弹销毁时间
    /// --武器技能
    /// ----反弹
    /// ----触发爆炸
    /// ----围绕玩家旋转
    ///
    /// 玩家属性
    /// --？？？
    /// ----？？？
    /// ----？？？
    /// ----？？？
    /// --？？？
    /// ----？？？
    /// ----？？？
    /// ----？？？
    /// 
    /// 敌人系统
    /// --？？？
    /// ----？？？
    /// ----？？？
    /// ----？？？
    /// --？？？
    /// ----？？？
    /// ----？？？
    /// ----？？？
    
    
    public class LightTankGameManager : BaseSingleton<LightTankGameManager>
    {

        [Header("Skill Config")]
        public bool isGuardBullet;
        public bool isAutoBullet;
        public bool isReflectionBullet;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}