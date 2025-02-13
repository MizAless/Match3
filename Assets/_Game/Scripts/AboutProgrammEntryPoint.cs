using _Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class AboutProgrammEntryPoint : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;

        private void Start()
        {
            _menuButton.onClick.AddListener(() => SceneLoader.Load(Scene.MainMenu));
        }
    }
}