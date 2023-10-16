using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameEntities
{
    public partial struct FireSystem : ISystem
    {
        private float _timer;
        private float _speed;
        private float _dist;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<UIData>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<Fire>();
            _timer = 0f;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var delta = SystemAPI.Time.DeltaTime;

            var fire = SystemAPI.GetSingleton<Fire>();
            var ui = SystemAPI.GetSingleton<UIData>();
            _speed = ui.CurSpeed;
            _dist = ui.CurDist;

            if (_timer >= fire.FireSpeed)
            {
                if (Input.GetMouseButton(0))
                {
                    var entity = SystemAPI.GetSingletonEntity<Fire>();
                    var initTrans = state.EntityManager.GetComponentData<LocalToWorld>(entity);

                    if (ui.CurSkillNum == 1)
                    {
                        GenerateLineBullet(ref state, fire, initTrans);
                    }

                    if (ui.CurSkillNum == 2)
                    {
                        GenerateGuardBullet(ref state, fire, initTrans);
                    }

                    if (ui.CurSkillNum == 3)
                    {
                        GenerateAutoAttackBullet(ref state, fire, initTrans);
                    }
                }

                _timer = 0;
            }

            _timer += delta;
        }
        
        private void GenerateLineBullet(ref SystemState state, Fire fire , LocalToWorld trans)
        {
            var bullet = state.EntityManager.Instantiate(fire.LineBulletGo);
            GenerateBaseBullet(ref state, fire, bullet, trans, 0);
        }

        private void GenerateGuardBullet(ref SystemState state, Fire fire, LocalToWorld trans)
        {
            var bullet = state.EntityManager.Instantiate(fire.GuardBulletGo);
            GenerateBaseBullet(ref state, fire, bullet, trans, 0);

            var total = 0;
            if (SystemAPI.TryGetSingleton<GuardPathSpawner>(out var guardPathSpawner))
            {
                total = guardPathSpawner.Total;
            }
            state.EntityManager.SetComponentData(bullet, new FollowMove
            {
                MaxTargetIndex = total,
                MoveSpeed = fire.Force,
                RotateSpeed = fire.Force,
                TargetIndex = 0,
                StopDistance = 0.8f,
            });
            state.EntityManager.SetComponentEnabled<FollowMove>(bullet, false);
        }
        
        private void GenerateAutoAttackBullet(ref SystemState state, Fire fire , LocalToWorld trans)
        {
            var bullet = state.EntityManager.Instantiate(fire.AutoAttackBulletGo);
            GenerateBaseBullet(ref state, fire, bullet, trans, 0);

            if (SystemAPI.TryGetSingletonEntity<AutoAttackCluster>(out var cluster))
            {
                state.EntityManager.SetComponentData(bullet, new AutoAttackBullet
                {
                    Target = cluster,
                    Cluster = cluster,
                    Status = 0
                });
            }
        }

        private void GenerateBaseBullet(ref SystemState state, Fire fire, Entity bullet, LocalToWorld trans, int endDestroy)
        {
            state.EntityManager.SetComponentData(bullet, new MoveWithDirection
            {
                InitPos = trans.Position,
                MoveDir = trans.Forward,
                Velocity = 1 + (fire.Force - 1) * _speed ,
            });

            state.EntityManager.SetComponentData(bullet, new LocalTransform
            {
                Position = trans.Position,
                Rotation = trans.Rotation,
                Scale = 1
            });

            var destroyTime = 5f;
            if (fire.DestroyTime > 0)
            {
                destroyTime = fire.DestroyTime;
            }
            state.EntityManager.SetComponentData(bullet, new MaxLifeTime
            {
                DestroyTime = destroyTime,
                Current = 0
            });

            var moveDistance = 10f;
            if (fire.DestroyDistance >= 0)
            {
                moveDistance = fire.DestroyDistance;
            }
            state.EntityManager.SetComponentData(bullet, new MaxMoveDistance
            {
                DestroyDistance = 1f + moveDistance * _dist ,
                EndDestroy = endDestroy,
                Current = 0
            });
        }
    }
}