using _Game.Scripts.Game;
using _Game.Scripts.Game.Score;
using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Text _newRecordLabel;
        [SerializeField] private Button _continueButton;

        private PauseService _pauseService;
        private Leaderboard.Leaderboard _leaderboard;

        private void Awake()
        {
            _leaderboard = ServiceLocator.GetInstance<Leaderboard.Leaderboard>();
            _pauseService = ServiceLocator.GetInstance<PauseService>();
        }

        public void Init()
        {
            if (_leaderboard.HasNewRecord)
            {
                _newRecordLabel.gameObject.SetActive(true);
                _continueButton.onClick.AddListener(() => GoToScene(Scene.Leaderboard));
            }
            else
            {
                _continueButton.onClick.AddListener(() => GoToScene(Scene.Leaderboard));
            }
        }

        public void SetScore(int score)
        {
            _scoreView.SetText($"Score: {score.ToString()}");
        }

        private void GoToScene(Scene scene)
        {
            _pauseService.IsPaused = false;
            SceneLoader.Load(Scene.Leaderboard);
        }
    }
}