using System;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Input
{
    public class PlayerInput : IPlayerInput, ITickable
    {
        public event Action<IClickable> Clicked;
        
        private readonly Camera _camera = Camera.main;

        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

                Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

                if (colliders.Length <= 0)
                    return;

                foreach (Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent(out IClickable clickable))
                    {
                        Clicked?.Invoke(clickable);
                    }
                }
            }
            //
            // if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            // {
            //     ServiceLocator.GetInstance<Game.Game>().Stop();
            // }
        }
    }
}