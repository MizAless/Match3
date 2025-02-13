using System;
using System.Collections.Generic;
using _Game.Scripts.Balls;
using _Game.Scripts.Game.RemainingMoves;
using _Game.Scripts.Game.Score;
using _Game.Scripts.Leaderboard;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Tools
{
    public static class Prefabs
    {
        private const string PrefabsFolder = "Prefabs";

        private static readonly Dictionary<Type, string> _prefabs = new Dictionary<Type, string>()
        {
            { typeof(Ball), nameof(Ball) },
            { typeof(BallsGrid), nameof(BallsGrid) },
            { typeof(AcceptPopup), nameof(AcceptPopup) },
            { typeof(ScoreView), nameof(ScoreView) },
            { typeof(RemainingMovesView), nameof(RemainingMovesView) },
            { typeof(GameOverScreenPresenter), nameof(GameOverScreenPresenter) },
            { typeof(RowView), "ElementView" },
        };

        public static T Load<T>()
            where T : MonoBehaviour
        {
            return Resources.Load<T>($"{PrefabsFolder}/{_prefabs[typeof(T)]}");
        }
    }
}