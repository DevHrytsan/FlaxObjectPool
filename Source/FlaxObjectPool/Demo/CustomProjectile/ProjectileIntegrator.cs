using System;
using System.Collections.Generic;
using FlaxEditor.Content.Settings;
using FlaxEngine;

namespace FlaxObjectPool.Demo
{
    /// <summary>
    /// ProjectileIntegrator class.
    /// </summary>
    public static class ProjectileIntegrator
    {
        public static Vector3 gravityVector = new Vector3(0, -9.81, 0);
        public static void IntegrateHeuns(ProjectileData data, float timeStep, Vector3 currentPos, Vector3 currentVel, out Vector3 newPos, out Vector3 newVel)
        {
            //Calculate simple drag
            Vector3 dragVector = data.dragFactor * -currentVel * UnitConstants.CmToM;
            //Add all factors that affects the acceleration
            Vector3 accelerationFactor = gravityVector + dragVector;
            //Find new position and new velocity with Forward Euler
            Vector3 newVelEuler = currentVel + timeStep * accelerationFactor;

            //Heuns method's final step if acceleration is constant
            newVel = currentVel + timeStep * 0.5f * (accelerationFactor + accelerationFactor);
            newPos = currentPos + timeStep * 0.5f * (currentVel + newVelEuler);
        }
    }
}
