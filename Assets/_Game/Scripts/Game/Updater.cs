using System.Collections.Generic;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Game
{
    public class Updater : MonoBehaviour
    {
        private readonly List<ITickable> _tickableEntities = new List<ITickable>();
        private PauseService _pauseService;

        private void Start()
        {
            _pauseService = ServiceLocator.GetInstance<PauseService>();
        }

        public void Register(ITickable entity)
        {
            _tickableEntities.Add(entity);
            enabled = true;
        }

        private void Update()
        {
            if (_tickableEntities.Count == 0)
                enabled = false;

            if (_pauseService.IsPaused)
                return;

            foreach (var entity in _tickableEntities)
                entity.Tick();
        }
    }
}