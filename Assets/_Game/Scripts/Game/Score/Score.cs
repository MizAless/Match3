using System;
using System.Collections.Generic;
using _Game.Scripts.Balls;

namespace _Game.Scripts.Game.Score
{
    public class Score
    {
        private readonly Destroyer _destroyer;

        private int _additionalValuePerBall = 100;

        public Score(Destroyer destroyer)
        {
            _destroyer = destroyer;
            
            _destroyer.BallsDestroyed += OnBallsDestroyed;
        }

        public event Action Changed;
        public int Value { get; private set; } = 0;

        private void OnBallsDestroyed(IReadOnlyList<Ball> balls)
        {
            Value += balls.Count * _additionalValuePerBall;
            Changed?.Invoke();
        }
    }
}