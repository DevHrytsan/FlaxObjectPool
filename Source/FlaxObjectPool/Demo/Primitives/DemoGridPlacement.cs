using System;
using System.Collections.Generic;
using FlaxEngine;

namespace FlaxObjectPool.Demo
{
    /// <summary>
    /// Just simple grid generator by grid.
    /// </summary>
    public class DemoGridPlacement : Script
    {
        public Prefab genPrefab;
        public Float3 size = new Float3(1, 1, 1);
        public Float3 gridSize = new Float3(3, 3, 3);

        /// <inheritdoc/>
        public override void OnStart()
        {
            for (int x = 0; x < gridSize.X; x++)
            {
                for (int y = 0; y < gridSize.Y; y++)
                {
                    for (int z = 0; z < gridSize.Z; z++)
                    {
                        Actor primActor = PrefabManager.SpawnPrefab(genPrefab, Actor);
                        primActor.Name = (x > y) ? "x" + x : "y" + y;
                        primActor.Position = new Vector3(Actor.Position.X + x * size.X * UnitConstants.MToCm, Actor.Position.Y + z * size.Y * UnitConstants.MToCm, Actor.Position.Z + y * size.Z * UnitConstants.MToCm);
                    }
                }
            }
        }
    }


}
