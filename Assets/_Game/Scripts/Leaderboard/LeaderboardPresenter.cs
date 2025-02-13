using System;
using System.Collections.Generic;
using System.Linq;

namespace _Game.Scripts.Leaderboard
{
    public class LeaderboardPresenter : IDisposable
    {
        private LeaderboardView _leaderboardView;
        private Leaderboard _leaderboard;
        private RowView _headersRow;
        private RowFabric _rowFabric;
        private List<RowView> _rows = new List<RowView>();

        public LeaderboardPresenter(LeaderboardView leaderboardView, Leaderboard leaderboard, RowFabric rowFabric)
        {
            _leaderboardView = leaderboardView;
            _rowFabric = rowFabric;
            _leaderboard = leaderboard;

            _headersRow = _rowFabric.CreateRowWith("Score", "Date");
            _leaderboardView.AddElement(_headersRow);

            _leaderboard.ElementsLoaded += OnElementsLoaded;
        }

        public void Dispose()
        {
            _leaderboard.ElementsLoaded -= OnElementsLoaded;
        }

        private void OnElementsLoaded(IReadOnlyCollection<LeaderboardElement> elements)
        {
            if (elements == null || elements.Count == 0)
                return;

            var sortedElements = elements.OrderBy(element => element.Score).Reverse().ToList();

            foreach (var element in sortedElements)
            {
                var row = _rowFabric.CreateRow(element);
                _rows.Add(row);
                _leaderboardView.AddElement(row);
            }
            
            if (_leaderboard.HasNewRecord)
            {
                _leaderboardView.Highlight(_rows.First());
                _leaderboard.HasNewRecord = false;
            }
        }
    }
}