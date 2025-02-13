using _Game.Scripts.Tools;
using _Game.Scripts.UI;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Game
{
    public class Exiter
    {
        private AcceptPopup _acceptPopupPrefab;

        private AcceptPopup _acceptPopup;

        private Canvas _canvas;

        public Exiter(Canvas canvas)
        {
            _acceptPopupPrefab = Prefabs.Load<AcceptPopup>();
            _canvas = canvas;
        }

        public void TryExit()
        {
            if (_acceptPopup == null)
                _acceptPopup = Object.Instantiate(_acceptPopupPrefab, _canvas.transform);
            else if (_acceptPopup.isActiveAndEnabled == false)
                _acceptPopup.Enable();

            _acceptPopup.AddAction(Exit);
        }

        private void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}