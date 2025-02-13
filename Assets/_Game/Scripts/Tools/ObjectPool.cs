using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Tools
{
    public class ObjectPool<T>
        where T : MonoBehaviour
    {
        private readonly Transform _container;
        private readonly T _prefab;

        private Queue<T> _pool = new Queue<T>();

        public ObjectPool(Transform container, T prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public T Get()
        {
            T newObject;

            if (_pool.Count == 0)
            {
                newObject = Object.Instantiate(_prefab, _container);
                return newObject;
            }

            newObject = _pool.Dequeue();
            newObject.gameObject.SetActive(true);

            return newObject;
        }

        public void Put(T putedObject)
        {
            _pool.Enqueue(putedObject);
            putedObject.transform.parent = _container;
            putedObject.gameObject.SetActive(false);
        }
    }
}