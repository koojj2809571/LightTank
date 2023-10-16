using System;
using Manager;
using Unity.Entities;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public class FireAuthoring : MonoBehaviour
    {
        public float fireSpeed;
        public int fireNumEveryTime;
        public GameObject lineBulletGo;
        public GameObject guardBulletGo;
        public GameObject autoAttackBulletGo;
        public float force;
        public float destroyTime;
        public float destroyDistance;

        class Baker: Baker<FireAuthoring>
        {
            public override void Bake(FireAuthoring authoring)
            {
                var lineBulletEntity = GetEntity(authoring.lineBulletGo, TransformUsageFlags.Dynamic);
                var guardBulletEntity = GetEntity(authoring.guardBulletGo, TransformUsageFlags.Dynamic);
                var autoAttackBulletEntity = GetEntity(authoring.autoAttackBulletGo, TransformUsageFlags.Dynamic);
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<TankMuzzleTag>(entity);
                AddComponent(entity, new Fire
                {
                    LineBulletGo = lineBulletEntity,
                    GuardBulletGo = guardBulletEntity,
                    AutoAttackBulletGo = autoAttackBulletEntity,
                    FireSpeed = authoring.fireSpeed,
                    FireNumEveryTime = authoring.fireNumEveryTime,
                    Force = authoring.force,
                    DestroyTime = authoring.destroyTime,
                    DestroyDistance = authoring.destroyDistance
                });
            }
        }

    }
}