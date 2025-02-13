using UnityEngine;

namespace _Game.Scripts.Game.RemainingMoves
{
    public class RemainingMovesController
    {
        private RemainingMoves _remainingMoves;
        private RemainingMovesView _remainingMovesView;
        private RectTransform _gameScreen;
        private RectTransform _gameOverScreen;

        public RemainingMovesController(RemainingMoves remainingMoves, RemainingMovesView remainingMovesView)
        {
            _remainingMoves = remainingMoves;
            _remainingMovesView = remainingMovesView;
            SetText();

            _remainingMoves.Changed += SetText;
        }

        private void SetText()
        {
            _remainingMovesView.SetText($"Remaining moves: {_remainingMoves.Value}");
        }
    }
}