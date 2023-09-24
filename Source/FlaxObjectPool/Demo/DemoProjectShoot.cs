using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using FlaxEngine;

namespace FlaxObjectPool.Demo
{
    /// <summary>
    /// This script demonstrates how you can utilize an object pool in your project.
    /// </summary>
    public class DemoProjectShoot : Script
    {
        #region Public
        public Prefab spawnObject; //Projectile object
        public float fireRate = 10f; //Fire rate of system per second
        public float velocity = 10f; //In m/s
        public float deviation = 0.2f; //Spread 
        #endregion
        #region Private
        private BaseObjectPool<Actor> _objectPool;
        private RandomStream _random;
        private float _lastFired;
        private Actor _sceneActor;
        #endregion
        #region Public Functions
        public override void OnStart()
        {
            _sceneActor = Level.FindActor<Scene>();

            _random = new RandomStream();
            //Initialize pool
            _objectPool = new BaseObjectPool<Actor>(PoolPreload, PoolGet, PoolRelease, PoolDestroy, 64, 128, true);
        }

        public override void OnFixedUpdate()
        {
            if (Input.GetMouseButton(MouseButton.Left) && Time.GameTime - _lastFired > 1 / fireRate)
            {
                _lastFired = Time.GameTime;

                Actor objectSpawn = _objectPool.Get(); //Firstly, retrieve object from Pool

                //Simulates spread of gun
                Float3 deviationAngle = Float3.Zero;
                deviationAngle.X = _random.RandRange(-deviation, deviation);
                deviationAngle.Y = _random.RandRange(-deviation, deviation);
                FlaxEngine.Quaternion deviationRotation = FlaxEngine.Quaternion.Euler(deviationAngle);
                var direction = Actor.Direction * deviationRotation;

                //Getting Projectile script
                var projectile = objectSpawn.GetScript<Projectile>();
                if (projectile) projectile.SetStartValues(Actor.Position, direction.Normalized * velocity * UnitConstants.MToCm);

            }

            if (Input.GetMouseButtonDown(MouseButton.Middle))
            {
                _objectPool.Clean(); //Calling clean for remove all active pooled object
            }
        }

        public void HandleHit(RayCastHit hit, Actor by)
        {
            _objectPool.Release(by);
        }
        #endregion
        #region Pool Methods
        public Actor PoolPreload()
        {
            Actor actor = PrefabManager.SpawnPrefab(spawnObject, Actor);

            Projectile projectile = actor.GetScript<Projectile>();
            if (projectile) projectile.OnHit += HandleHit;

            return actor;
        }

        public void PoolDestroy(Actor actor)
        {
            Projectile projectile = actor.GetScript<Projectile>();
            if (projectile) projectile.OnHit -= HandleHit;

            Destroy(actor);
        }
        public void PoolGet(Actor actor)
        {
            actor.IsActive = true;
            actor.Parent = _sceneActor;

        }
        public void PoolRelease(Actor actor)
        {
            actor.IsActive = false;
            actor.Parent = Actor;

        }
        #endregion
    }
}
