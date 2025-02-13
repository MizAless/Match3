using _Game.Scripts.Game;
using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _aboutProgrammButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            var exiter = new Exiter(_canvas);

            _newGameButton.onClick.AddListener(() => SceneLoader.Load(Scene.Gameplay));
            _leaderboardButton.onClick.AddListener(() => SceneLoader.Load(Scene.Leaderboard));
            _aboutProgrammButton.onClick.AddListener(() => SceneLoader.Load(Scene.AboutProgramm));
            _exitButton.onClick.AddListener(exiter.TryExit);
        }
    }
}