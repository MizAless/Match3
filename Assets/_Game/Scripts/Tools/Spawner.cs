using System;
using _Game.Scripts.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Tools
{
    public class Spawner<T>
        where T : MonoBehaviour, IDisalable<T>
    {
        private readonly T _prefab;
        private readonly ObjectPool<T> _pool;

        private Transform _container;

        public event Action<T> ObjectSpawned;

        public Spawner(T prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
            _pool = new ObjectPool<T>(_container, _prefab);
        }

        public virtual T Spawn()
        {
            T newObject = _pool.Get();

            newObject.Disabled += OnDisabled;

            ObjectSpawned?.Invoke(newObject);

            return newObject;
        }

        private void OnDisabled(T obj)
        {
            obj.Disabled -= OnDisabled;
            _pool.Put(obj);
        }
    }
}