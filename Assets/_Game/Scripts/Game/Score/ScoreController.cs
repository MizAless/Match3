namespace _Game.Scripts.Game.Score
{
    public class ScoreController
    {
        private Score _score;
        private ScoreView _scoreView;

        public ScoreController(Score score, ScoreView scoreView)
        {
            _score = score;
            _scoreView = scoreView;
            _scoreView.SetText($"Score: {_score.Value}");
            
            _score.Changed += OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            _scoreView.SetText($"Score: {_score.Value}");
        }
    }
}