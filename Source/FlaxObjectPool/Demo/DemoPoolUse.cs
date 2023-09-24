using System;
using System.Collections.Generic;
using FlaxEngine;

namespace FlaxObjectPool.Demo
{
    /// <summary>
    /// DemoPoolUse Script.
    /// </summary>
    public class DemoPoolUse : Script
    {
        public Prefab spawnObject;

        [ShowInEditor] private BaseObjectPool<Actor> objectPool;
        public override void OnStart()
        {
            objectPool = new BaseObjectPool<Actor>(PoolPreload, PoolGet, PoolRelease, PoolDestroy, 32, 64);
        }

        public override void OnEnable()
        {
        

        }

        public override void OnDisable()
        {

        }

        public override void OnLateUpdate()
        {
            if (Input.GetMouseButtonDown(MouseButton.Left))
            {
                Actor poolItem = objectPool.Get();
                //poolItem.Position = Vector3.One;


            }
        }

        #region Pool Methods
        public Actor PoolPreload()
        {
            return PrefabManager.SpawnPrefab(spawnObject, Actor);
        }

        public void PoolDestroy(Actor actor)
        {
            Destroy(actor);
        }
        public void PoolGet(Actor actor)
        {
            actor.IsActive = true;
        }
        public void PoolRelease(Actor actor)
        {
            actor.IsActive = false;
        }

        #endregion
    }
}
