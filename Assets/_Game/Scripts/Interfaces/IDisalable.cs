using System;
using UnityEngine;

namespace _Game.Scripts.Interfaces
{
    public interface IDisalable<T> 
        where T : MonoBehaviour
    {
        public event Action<T> Disabled;

        public void Disable();
    }
}
