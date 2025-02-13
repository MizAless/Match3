using System;
using System.Collections.Generic;
using _Game.Scripts.Game;
using _Game.Scripts.Tools;
using DG.Tweening;

namespace _Game.Scripts.Balls
{
    public class Destroyer
    {
        private readonly Finder _finder;

        public Destroyer(Finder finder)
        {
            _finder = finder;
        }

        public event Action<IReadOnlyList<Ball>> BallsDestroyed;

        public void Destroy(Ball ball)
        {
            ServiceLocator.GetInstance<PauseService>().IsPaused = true;
            
            List<Ball> _neighboringBalls = _finder.FindNeighborsBalls(ball);
            List<Ball> _destroyedBalls = new List<Ball> { ball };

            if (_neighboringBalls.Count > 1)
                _destroyedBalls.AddRange(_neighboringBalls);

            Sequence destroySequence = DOTween.Sequence();

            float delayBetweenBalls = 0.1f;
            float currentDelay = 0;
            
            foreach (var destroyedBall in _destroyedBalls)
            {
                destroySequence.Join(destroyedBall.transform.DOMove(ball.transform.position, 0.2f)
                    .SetEase(Ease.InOutQuad));

                destroySequence.Join(destroyedBall.transform.DOScale(destroyedBall.transform.localScale * 1.5f, 0.2f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.InBounce)
                    .OnComplete(() => destroyedBall.Disable()));
            }

            destroySequence.OnComplete(() => BallsDestroyed?.Invoke(_destroyedBalls));
            destroySequence.Play();
        }
    }
}