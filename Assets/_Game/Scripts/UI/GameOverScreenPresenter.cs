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

        public void Init()
        {
            if (ServiceLocator.GetInstance<Leaderboard.Leaderboard>().HasNewRecord)
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
            ServiceLocator.GetInstance<PauseService>().IsPaused = false;
            SceneLoader.Load(Scene.Leaderboard);
        }
    }
}