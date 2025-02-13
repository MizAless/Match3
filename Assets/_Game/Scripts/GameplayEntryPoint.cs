using _Game.Scripts.Balls;
using _Game.Scripts.Game;
using _Game.Scripts.Input;
using _Game.Scripts.Tools;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private Updater _updater;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _menuButton;
        [SerializeField] private RectTransform _gameScreen;

        private UIManager _uiManager;
        private Game.Game _game;
        private PlayerInput _playerInput;
        private BallsGrid _ballsGrid;
        private Spawner<Ball> _ballSpawner;

        private void Start()
        {
            CreateInitialComponents();
            InstallBindings();

            _playerInput = new PlayerInput();
            _game = new Game.Game(_playerInput, _ballsGrid);
            ServiceLocator.Register(_game);
            _uiManager = new UIManager(_canvas, _gameScreen, _menuButton, _game);

            _updater.Register(_playerInput);
        }

        private void CreateInitialComponents()
        {
            _ballsGrid = Instantiate(Prefabs.Load<BallsGrid>());
            _ballSpawner = new Spawner<Ball>(Prefabs.Load<Ball>(), _ballsGrid.transform);
        }

        private void InstallBindings()
        {
            ServiceLocator.Register(_updater);
            ServiceLocator.Register(_playerInput);
            ServiceLocator.Register(_ballsGrid);
            ServiceLocator.Register(_ballSpawner);
        }
    }
}