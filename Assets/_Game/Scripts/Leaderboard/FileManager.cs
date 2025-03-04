using System;
using System.IO;
using _Game.Scripts.Tools;

namespace _Game.Scripts.Leaderboard
{
    public static class FileManager
    {
        private const string FileName = "records.csv";
        
        private static readonly Leaderboard _leaderboard;

        static FileManager()
        {
            _leaderboard = ServiceLocator.GetInstance<Leaderboard>();
        }

        public static void Save(int score)
        {
            string date = DateTime.Now.ToString();
            
            string record = $"{date};{score}";
            
            try
            {
                using StreamWriter writer = new StreamWriter(FileName, true);
                writer.WriteLine(record);
                _leaderboard.HasNewRecord = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }
        
        public static string[] Load()
        {
            if (!File.Exists(FileName))
            {
                throw new Exception($"file {FileName} not exist");
            }

            return File.ReadAllLines(FileName);
        }
    }
}