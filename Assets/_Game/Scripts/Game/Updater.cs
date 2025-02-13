using System.Collections.Generic;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Game
{
    public class Updater : MonoBehaviour
    {
        private List<ITickable> _tickableEntities = new List<ITickable>();

        public void Register(ITickable entity)
        {
            _tickableEntities.Add(entity);
            enabled = true;
        }

        private void Update()
        {
            if (_tickableEntities.Count == 0)
                enabled = false;
            
            if (ServiceLocator.GetInstance<PauseService>().IsPaused)
                return;

            foreach (var entity in _tickableEntities)
                entity.Tick();
        }
    }
}