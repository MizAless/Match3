using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Balls;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Leaderboard;
using _Game.Scripts.Tools;

namespace _Game.Scripts.Game
{
    public class Game
    {
        private readonly IPlayerInput _playerInput;
        private readonly BallsGrid _ballsGrid;
        private readonly Destroyer _destroyer;
        private readonly Finder _finder;
        private readonly Colorizer _colorizer;
        private readonly Leaderboard.Leaderboard _leaderboard;
        private readonly Tutorial _tutorial;

        private int _startRemainingMoves = 3;

        public event Action GameOvered;

        public Game(IPlayerInput playerInput, BallsGrid ballsGrid)
        {
            _leaderboard = ServiceLocator.GetInstance<Leaderboard.Leaderboard>();

            _playerInput = playerInput;
            _ballsGrid = ballsGrid;
            _colorizer = new Colorizer();
            _finder = new Finder(_ballsGrid);
            _destroyer = new Destroyer(_finder);
            _tutorial = new Tutorial(_ballsGrid, _finder, _colorizer);
            Score = new Score.Score(_destroyer);
            RemainingMoves = new RemainingMoves.RemainingMoves(_startRemainingMoves, _destroyer);

            _ballsGrid.Init();
            _ballsGrid.Fill();
            _tutorial.HighlightFirstMove();

            GameOvered += OnGameOvered;
            RemainingMoves.Ended += OnMovesEnded;
            _playerInput.Clicked += OnClicked;
            _destroyer.BallsDestroyed += OnBallsBallsDestroyed;
        }

        public Score.Score Score { get; private set; }
        public RemainingMoves.RemainingMoves RemainingMoves { get; private set; }

        public void Stop()
        {
            GameOvered?.Invoke();
        }

        private void OnMovesEnded()
        {
            GameOvered?.Invoke();
        }

        private void OnGameOvered()
        {
            ServiceLocator.GetInstance<PauseService>().IsPaused = true;

            var maxScore = 0;

            maxScore = _leaderboard.GetHighScore();

            if (maxScore < Score.Value)
                FileManager.Save(Score.Value);
        }

        private void OnBallsBallsDestroyed(IReadOnlyList<Ball> destroyedBalls)
        {
            int minActiveCol = destroyedBalls.Min(ball => ball.Coordinates.y);
            int maxActiveCol = destroyedBalls.Max(ball => ball.Coordinates.y);
            int minActiveRow = destroyedBalls.Min(ball => ball.Coordinates.x);

            _ballsGrid.Refresh(minActiveCol, maxActiveCol, minActiveRow);
        }

        private void OnClicked(IClickable clickable)
        {
            if (clickable is Ball ball)
            {
                if (!CheckValidFirstMove(ball))
                    return;

                _destroyer.Destroy(ball);
            }
        }
        
        private bool CheckValidFirstMove(Ball ball)
        {
            if (_tutorial.IsFirstMoveOccurred)
                return true;
            
            if (_tutorial.GetTargetBall() != ball)
                return false;
            
            _tutorial.IsFirstMoveOccurred = true;
            return true;
        }
    }
}