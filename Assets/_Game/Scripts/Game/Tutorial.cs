using System.Collections.Generic;
using _Game.Scripts.Balls;

namespace _Game.Scripts.Game
{
    public class Tutorial
    {
        private readonly BallsGrid _ballsGrid;
        private readonly Finder _finder;
        private readonly Colorizer _colorizer;
        
        private Ball _targetBall;

        public Tutorial(BallsGrid ballsGrid, Finder finder, Colorizer colorizer)
        {
            _ballsGrid = ballsGrid;
            _finder = finder;
            _colorizer = colorizer;
        }

        public bool IsFirstMoveOccurred = false;

        public void HighlightFirstMove()
        {
            var randomColor = _colorizer.GetRandomColor();

            var middleCol = _ballsGrid.Columns / 2;
            var middleRow = _ballsGrid.Rows / 2;

            var middleBall = _ballsGrid.Matrix[middleRow, middleCol];

            List<Ball> middleClaster = new List<Ball>() { middleBall };

            middleClaster.Add(_ballsGrid.Matrix[middleRow + 1, middleCol]);
            middleClaster.Add(_ballsGrid.Matrix[middleRow - 1, middleCol]);

            _colorizer.SetColorToBalls(middleClaster, randomColor);

            middleClaster = _finder.FindNeighborsBalls(middleBall);

            _targetBall = middleBall;
            middleBall.SetShacking();
            middleBall.SetHighlighting();
            middleClaster.ForEach(ball => ball.SetHighlighting());
        }

        public Ball GetTargetBall()
        {
            return _targetBall;
        }
    }
}