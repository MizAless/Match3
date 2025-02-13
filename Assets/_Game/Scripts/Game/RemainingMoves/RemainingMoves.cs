using System;
using System.Collections.Generic;
using _Game.Scripts.Balls;

namespace _Game.Scripts.Game.RemainingMoves
{
    public class RemainingMoves
    {
        private readonly Destroyer _destroyer;
        private int _value;

        public RemainingMoves(int value, Destroyer destroyer)
        {
            Value = value;
            _destroyer = destroyer;

            _destroyer.BallsDestroyed += OnBallsDestroy;
        }

        public int Value
        {
            get => _value;
            private set
            {
                _value = value;

                if (_value <= 0)
                {
                    _value = 0;
                    Ended?.Invoke();
                }
                
                Changed?.Invoke();
            }
        }

        public event Action Changed;
        public event Action Ended;
        
        private void OnBallsDestroy(IReadOnlyList<Ball> balls)
        {
            switch (balls.Count)
            {
                case 1:
                    Value--;
                    break;
                case 3:
                    Value += 2;
                    break;
                case 4:
                    Value += 3;
                    break;
                case >= 5:
                    Value += 4;
                    break;
            }
        }
    }
}