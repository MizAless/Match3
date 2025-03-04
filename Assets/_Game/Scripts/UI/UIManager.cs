using _Game.Scripts.Game;
using _Game.Scripts.Game.RemainingMoves;
using _Game.Scripts.Game.Score;
using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class UIManager
    {
        private readonly AcceptPopup _acceptPopupPrefab;
        private readonly GameOverScreenPresenter _gameOverScreenPrefab;

        private Button _menuButton;
        private Canvas _canvas;
        private ScoreView _scoreView;
        private RemainingMovesView _remainingMovesView;
        private AcceptPopup _acceptPopup;
        private RectTransform _gameScreen;
        private Game.Game _game;

        private ScoreController _scoreController;
        private RemainingMovesController _remainingMovesController;
        
        private PauseService _pauseService;

        public UIManager(Canvas canvas, RectTransform gameScreen, Button menuButton, Game.Game game)
        {
            _pauseService = ServiceLocator.GetInstance<PauseService>();
            
            _gameOverScreenPrefab = Prefabs.Load<GameOverScreenPresenter>();
            _acceptPopupPrefab = Prefabs.Load<AcceptPopup>();

            _canvas = canvas;
            _gameScreen = gameScreen;
            _menuButton = menuButton;
            _game = game;

            _scoreView = Object.Instantiate(Prefabs.Load<ScoreView>(), _gameScreen.transform);
            _remainingMovesView = Object.Instantiate(Prefabs.Load<RemainingMovesView>(), _gameScreen.transform);

            _scoreController = new ScoreController(_game.Score, _scoreView);
            _remainingMovesController = new RemainingMovesController(_game.RemainingMoves, _remainingMovesView);

            _game.GameOvered += ShowGameOverScreen;
            _menuButton.onClick.AddListener(TryLoadMainMenuScene);
        }

        ~UIManager()
        {
            _game.GameOvered -= ShowGameOverScreen;
        }
        
        private void ShowGameOverScreen()
        {
            _gameScreen.gameObject.SetActive(false);
            
            var gameOverScreen = Object.Instantiate(_gameOverScreenPrefab, _canvas.transform);
            gameOverScreen.Init();
            gameOverScreen.SetScore(_game.Score.Value);
        }

        private void TryLoadMainMenuScene()
        {
            if (_acceptPopup == null)
                _acceptPopup = Object.Instantiate(_acceptPopupPrefab, _canvas.transform);
            else if (_acceptPopup.isActiveAndEnabled == false)
                _acceptPopup.Enable();

            _pauseService.IsPaused = true;

            _acceptPopup.AddAction(() => SceneLoader.Load(Scene.MainMenu),
                () => _pauseService.IsPaused = false);
        }
    }
}