using System.Globalization;
using _Game.Scripts.Tools;
using UnityEngine;

namespace _Game.Scripts.Leaderboard
{
    public class RowFabric
    {
        private RowView _rowPrefab;

        public RowFabric()
        {
            _rowPrefab = Prefabs.Load<RowView>();
        }

        public RowView CreateRow(LeaderboardElement leaderboardElement)
        {
            var newElement = Object.Instantiate(_rowPrefab);
            newElement.SetData(leaderboardElement.Score.ToString(),
                leaderboardElement.Date.ToString(CultureInfo.InvariantCulture));
            return newElement;
        }

        public RowView CreateRowWith(string value1, string value2)
        {
            var newElement = Object.Instantiate(_rowPrefab);
            newElement.SetData(value1, value2);
            return newElement;
        }
    }
}