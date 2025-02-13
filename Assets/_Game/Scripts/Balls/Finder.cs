using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Balls
{
    public class Finder
    {
        private BallsGrid _ballsGrid;

        public Finder(BallsGrid ballsGrid)
        {
            _ballsGrid = ballsGrid;
        }

        public List<Ball> FindNeighborsBalls(Ball ball)
        {
            List<Ball> neighbors = new List<Ball>() { /*ball*/ };

            for (int i = 1; i >= -1; i--)
            {
                for (int j = 1; j >= -1; j--)
                {
                    if (i == 0 && j == 0)
                        continue;

                    neighbors.AddRange(FindNeighborsInDirection(ball, new Vector2Int(i, j)));
                }
            }

            return neighbors;
        }

        private List<Ball> FindNeighborsInDirection(Ball ball, Vector2Int direction)
        {
            var row = ball.Coordinates.x;
            var col = ball.Coordinates.y;

            List<Ball> neighbors = new List<Ball>();
            Color targetColor = ball.Color;

            row += direction.x;
            col += direction.y;

            while (row >= 0 && row < _ballsGrid.Rows &&
                   col >= 0 && col < _ballsGrid.Columns)
            {
                Ball checkedBall = _ballsGrid.Matrix[row, col];

                row += direction.x;
                col += direction.y;

                if (checkedBall.Color == targetColor)
                    neighbors.Add(checkedBall);
                else
                    break;
            }

            return neighbors;
        }
    }
}