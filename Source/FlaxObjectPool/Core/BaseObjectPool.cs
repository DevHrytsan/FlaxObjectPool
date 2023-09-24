using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using FlaxEngine;

namespace FlaxObjectPool
{
    /// <summary>
    /// BaseObjectPool script helps manage instance of objects efficiently in your project.
    /// </summary>
    public class BaseObjectPool<T> where T : class
    {
        #region Private
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _actionOnGet;
        private readonly Action<T> _actionOnRelease;
        private readonly Action<T> _actionOnDestroy;

        private int _defaultCapacity;
        private int _maxSize;

        private readonly Queue<T> _pool = new Queue<T>();
        private readonly List<T> _activePools = new List<T>();

        private T _lastReleased;
        private bool _limitRelease;

        #endregion

        #region Public
        /// <summary>
        /// Default capacity
        /// </summary>
        public int DefaultCapacity
        {
            get => _defaultCapacity;
        }
        /// <summary>
        /// Max size of pool
        /// </summary>
        public int MaxSize
        {
            get => _maxSize;
        }
        /// <summary>
        /// The total number of active and inactive objects.
        /// </summary>
        public int CountAll
        {
            get => _pool.Count;
        }
        /// <summary>
        /// The number of active objects.
        /// </summary
        public int ActiveCount
        {
            get => _activePools.Count;
        }

        /// <summary>
        /// Returns Queue Pool
        /// </summary>
        public Queue<T> NotActivePool
        {
            get => _pool;
        }

        /// <summary>
        ///  Returns last released object
        /// </summary>
        public T LastReleased
        {
            get => _lastReleased;
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="preloadFunc">Utilized for generating instance and when the pool becomes empty</param>
        /// <param name="onGet">Triggered when an instance is retrieved from the pool.</param>
        /// <param name="onRelease">Invoked when an instance is returned to the pool</param>
        /// <param name="onDestroy">Called when an element cannot be returned to the pool because the pool has reached its maximum size.</param>
        /// <param name="defaultCapacity">Initial capacity for the pool when it is created.</param>
        /// <param name="maxSize">Maximum allowable size for the pool</param>
        /// <param name="limitRelease">Release objects if the active object count exceeds the maximum size</param>
        public BaseObjectPool(Func<T> preloadFunc, Action<T> onGet, Action<T> onRelease, Action<T> onDestroy, int defaultCapacity = 32, int maxSize = 64, bool limitRelease = false)
        {
            _preloadFunc = preloadFunc;
            _actionOnGet = onGet;
            _actionOnRelease = onRelease;
            _actionOnDestroy = onDestroy;
            _limitRelease = limitRelease;
            _defaultCapacity = defaultCapacity;
            _maxSize = maxSize;

            if (_defaultCapacity > _maxSize)
            {
                _maxSize = _defaultCapacity;
#if FLAX_EDITOR
                Debug.LogWarning("Default capacity higher than max size!");
#endif
            }

            if (_preloadFunc == null)
            {
#if FLAX_EDITOR
                Debug.LogError("No preload function. What are u doing?");
#endif
                return;
            }
            _activePools = new List<T>(_defaultCapacity);
            _pool = new Queue<T>(maxSize);

            for (int i = 0; i < _defaultCapacity; i++)
            {
                Release(preloadFunc());
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Get an instance from the pool. If pool is empty then a new instance will be created.
        /// </summary>
        /// <returns>Pooled item</returns>
        public T Get()
        {
            T item;

            if (_lastReleased != null)
            {
                item = _lastReleased;
                _lastReleased = null;
            }

            if (_limitRelease && (_activePools.Count < 0 || _activePools.Count > MaxSize))
            {
                Release(_activePools.First());
            }

            if (_pool.Count > 0)
            {
                item = _pool.Dequeue();
            }
            else
            {
                item = _preloadFunc();
            }

            _actionOnGet(item);
            _activePools.Add(item);

            return item;
        }
        /// <summary>
        /// Returns the instance back to the pool.
        /// </summary>
        /// <param name="item">Released item</param>
        public void Release(T item)
        {
            if (_pool.Count >= _maxSize)
            {
                Dispose(item);
                return;
            }

            if (_lastReleased == null)
            {
                _lastReleased = item;
            }

            _actionOnRelease(item);
            _pool.Enqueue(item);
            _activePools.Remove(item);


        }
        /// <summary>
        /// Call if u want to destroy whole pool
        /// </summary>
        public void Dispose()
        {
            foreach (T item in _pool.ToArray())
            {
                Dispose(item);
            }
        }

        private void Dispose(T item)
        {
            _activePools.Remove(item);
            _actionOnDestroy(item);
        }

        /// <summary>
        /// Call if u want to release whole pool
        /// </summary>
        public void Clean()
        {
            foreach (T item in _activePools.ToArray())
            {
                Release(item);
            }
        }
        #endregion
    }
}
