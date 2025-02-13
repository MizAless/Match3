using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Tools
{
    public static class SceneLoader
    {
        static SceneLoader()
        {
            foreach (var scene in (Scene[])Enum.GetValues(typeof(Scene)))
                _scenes.Add(scene, scene.ToString());
        }

        private static readonly Dictionary<Scene, string> _scenes = new Dictionary<Scene, string>();

        public static void Load(Scene scene)
        {
            SceneManager.LoadScene(_scenes[scene]);
        }
    }

    public enum Scene
    {
        Gameplay,
        Leaderboard,
        AboutProgramm,
        MainMenu,
    }
}