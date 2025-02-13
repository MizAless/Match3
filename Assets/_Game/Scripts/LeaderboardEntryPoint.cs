using System;
using _Game.Scripts.Leaderboard;
using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class LeaderboardEntryPoint : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private LeaderboardView _leaderboardView;

        private LeaderboardPresenter _leaderboardPresenter;

        private Leaderboard.Leaderboard _leaderboard;
        private RowFabric _rowFabric;

        private void Start()
        {
            _rowFabric = new RowFabric();
            _leaderboard = ServiceLocator.GetInstance<Leaderboard.Leaderboard>();
            _leaderboardPresenter = new LeaderboardPresenter(_leaderboardView, _leaderboard, _rowFabric);
            _leaderboard.LoadRecords();

            _menuButton.onClick.AddListener(() =>
            {
                _leaderboardPresenter.Dispose();
                SceneLoader.Load(Scene.MainMenu);
            });
        }
    }
}