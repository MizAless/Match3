using System.Collections.Generic;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Balls
{
    public class Colorizer
    {
        private readonly Color[] _availableColors = new[]
        {
            Color.green,
            Color.blue,
            Color.red,
            Color.gray,
            Color.black,
            // Color.yellow,
            // Color.cyan,
            // Color.magenta,
            // Color.white
        };

        private readonly BallsGrid _ballsGrid;
        private readonly Spawner<Ball> _ballSpawner;

        public Colorizer()
        {
            _ballsGrid = ServiceLocator.GetInstance<BallsGrid>();
            _ballSpawner = ServiceLocator.GetInstance<Spawner<Ball>>();
            
            _ballSpawner.ObjectSpawned += OnSpawned;
        }

        private void OnSpawned(Ball ball)
        {
            ball.ChangeColor(GetRandomColor());
        }
        
        public void SetColorToBalls(List<Ball> balls, Color color)
        {
            foreach (var ball in balls)
            {
                ball.ChangeColor(color);
            }
        }
        
        public Color GetRandomColor()
        {
            return _availableColors[Random.Range(0, _availableColors.Length)];
        }
    }
}