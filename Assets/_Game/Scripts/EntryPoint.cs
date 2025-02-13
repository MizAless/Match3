using _Game.Scripts.Game;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private Leaderboard.Leaderboard _leaderboard;
        private PauseService _pauseService;
        
        private void Start()
        {
            _leaderboard = new Leaderboard.Leaderboard();
            _pauseService = new PauseService();
            
            InstallBindings();
            
            SceneLoader.Load(Scene.MainMenu);
        }
        
        private void InstallBindings()
        {
            ServiceLocator.Init();
            ServiceLocator.Register(_leaderboard);
            ServiceLocator.Register(_pauseService);
        }
    }
}