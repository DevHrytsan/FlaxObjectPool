using System;
using System.Collections.Generic;
using FlaxEngine;

namespace FlaxObjectPool.Demo
{
    /// <summary>
    /// Demo Basic Custom Projectile Script.
    /// Basically, raycasting from previos frame position to current one and checking for collision.
    /// </summary>
    /// 
    [System.Serializable]
    public class ProjectileData
    {
        public float mass = 50;
        public float dragFactor = 0.05f;
    }
    public class Projectile : Script
    {
        public ProjectileData _projectileData;

        private Vector3 _lastPos;
        private Vector3 _lastVel;
        private Vector3 _currentPos;
        private Vector3 _currentVel;


        [HideInEditor] public Action<RayCastHit, Actor> OnHit;

        public override void OnFixedUpdate()
        {
            MoveBulletOneStep();
            HandleHit();
        }

        private void MoveBulletOneStep()
        {
            //Use an integration method to calculate the new position of the bullet
            float timeStep = 1 / Time.PhysicsFPS;

            _lastPos = _currentPos;
            _lastVel = _currentVel;

            ProjectileIntegrator.IntegrateHeuns(_projectileData, timeStep, _currentPos, _currentVel, out _currentPos, out _currentVel);

            //Add the new position to the Projectile
            Actor.Position = _currentPos;

            //To align the projectile with the velocity direction.
            Actor.Direction = _currentVel.Normalized;
        }
        private void HandleHit()
        {
            RayCastHit hit;

            if (Physics.LineCast(_currentPos, _lastPos, out hit))
            {
                if (hit.Collider)
                {
                    RigidBody hittedBody = hit.Collider.AttachedRigidBody;

                    if (hittedBody)
                    {
                        hittedBody.AddForceAtPosition(_projectileData.mass * _currentVel, hit.Point);
                    }
                    OnHit?.Invoke(hit, Actor);

                }
            }
        }

        /// <summary>
        /// Set the initial values of position and direction
        /// </summary>
        /// <param name="startPos">Representing a starting position.</param>
        /// <param name="startDir">Representing a starting direction</param>
        public void SetStartValues(Vector3 startPos, Vector3 startDir)
        {
            _lastPos = _currentPos = startPos;
            _lastVel = _currentVel = startDir;

            Actor.Position = startPos;
            Actor.Direction = _currentVel.Normalized;
        }

    }
}
