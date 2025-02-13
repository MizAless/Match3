using System;
using System.Collections.Generic;
using System.Linq;

namespace _Game.Scripts.Leaderboard
{
    public class Leaderboard
    {
        private List<LeaderboardElement> _cachedElements = new List<LeaderboardElement>();

        public event Action<IReadOnlyCollection<LeaderboardElement>> ElementsLoaded;

        private int _highScore = 0;

        public bool HasNewRecord = false;

        public void LoadRecords()
        {
            var parsed = ParseElements("Score", "Date");

            if (parsed == null)
                return;

            var elements = CreateElements(parsed);

            if (elements.Count > _cachedElements.Count)
                _cachedElements = elements;
                
            ElementsLoaded?.Invoke(_cachedElements);
        }

        public bool CheckIsAddedNew(LeaderboardElement element)
        {
            return !_cachedElements.Contains(element);
        }

        public int GetHighScore()
        {
            const string header = "Score";

            var parsed = ParseElements(header);

            if (parsed == null)
                return 0;

            var score = parsed[header].Max(int.Parse);

            if (_highScore < score)
                _highScore = score;

            return _highScore;
        }

        private List<LeaderboardElement> CreateElements(Dictionary<string, List<string>> parsed)
        {
            List<LeaderboardElement> elements = new List<LeaderboardElement>();

            var dates = parsed["Date"];
            var scores = parsed["Score"];

            for (int i = 0; i < dates.Count; i++)
            {
                var element = new LeaderboardElement();

                element.Date = DateTime.Parse(dates[i]);
                element.Score = int.Parse(scores[i]);
                
                elements.Add(element);
            }

            return elements;
        }

        private string[] GetLines()
        {
            string[] lines = FileManager.Load();

            return lines.Length < 2 ? null : lines;
        }

        private Dictionary<string, List<string>> ParseElements(params string[] headers)
        {
            var lines = GetLines();

            if (lines == null || lines.Length == 0)
                return null;

            Dictionary<string, List<string>> parsedLines = new Dictionary<string, List<string>>();

            foreach (var header in headers)
                parsedLines.Add(header, new List<string>());

            var headerIndices = new Dictionary<string, int>();
            var fileHeaders = lines[0].Split(';');

            for (int i = 0; i < fileHeaders.Length; i++)
                if (headers.Contains(fileHeaders[i]))
                    headerIndices[fileHeaders[i]] = i;

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(';');

                foreach (var header in headers)
                    if (headerIndices.TryGetValue(header, out int index))
                        parsedLines[header].Add(values[index]);
            }

            return parsedLines;
        }
    }
}