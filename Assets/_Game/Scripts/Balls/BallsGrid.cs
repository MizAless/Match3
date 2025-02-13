using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Balls
{
    public class BallsGrid : MonoBehaviour
    {
        [SerializeField] private float _spacing = 1.5f;

        private Ball[,] _matrix;
        private Vector2 _offset;
        private Spawner<Ball> _spawner;
        
        [field: SerializeField, Min(3)] public int Rows { get; private set; } = 5;
        [field: SerializeField, Min(3)] public int Columns { get; private set; } = 5;
        public Ball[,] Matrix => _matrix;
        
        public void Init()
        {
            _spawner = ServiceLocator.GetInstance<Spawner<Ball>>();
            _matrix = new Ball[Rows, Columns];
        }

        public void Fill()
        {
            _offset = new Vector2((Rows - 1) * _spacing, (Columns - 1) * _spacing) / -2f;

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Vector3 position = CalculatePosition(row, col);
                    Ball ball = _spawner.Spawn();
                    ball.Init(position, row, col);
                    _matrix[row, col] = ball;
                    ball.Disabled += OnBallDisabled;
                }
            }
        }

        public void Refresh(int minActiveCol, int maxActiveCol, int minActiveRow)
        {
            for (int col = minActiveCol; col <= maxActiveCol; col++)
            {
                int disabledBalls = 0;

                for (int row = minActiveRow; row < Rows; row++)
                {
                    if (_matrix[row, col] == null)
                        disabledBalls++;
                    else if (disabledBalls > 0)
                        Replace(_matrix[row, col], row - disabledBalls, col);
                }

                for (int row = Rows - disabledBalls; row < Rows; row++)
                {
                    Vector3 spawnPosition = CalculatePosition(row + disabledBalls, col);
                    Vector3 position = CalculatePosition(row, col);
                    Ball newBall = _spawner.Spawn();
                    newBall.Init(spawnPosition, row, col);
                    newBall.MoveTo(position);
                    _matrix[row, col] = newBall;
                }
            }
        }

        private void OnBallDisabled(Ball ball)
        {
            _matrix[ball.Coordinates.x, ball.Coordinates.y] = null;
        }

        private void Replace(Ball ball, int row, int col)
        {
            ball.SetCoordinates(new Vector2Int(row, col));
            ball.MoveTo(CalculatePosition(row, col));
            _matrix[row, col] = ball;
        }

        private Vector3 CalculatePosition(int row, int col)
        {
            return new Vector3(_offset.x + col * _spacing,
                _offset.y + row * _spacing, 0);
        }
    }
}