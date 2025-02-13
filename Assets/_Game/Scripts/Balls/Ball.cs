using System;
using _Game.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Balls
{
    public class Ball : MonoBehaviour, IDisalable<Ball>, IClickable
    {
        private Renderer _renderer;

        public Vector2Int Coordinates { get; private set; }
        public Color Color { get; private set; }

        public event Action<Ball> Disabled;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void Init(Vector2 position, int x, int y)
        {
            transform.position = position;
            SetCoordinates(new Vector2Int(x, y));
        }

        public void SetCoordinates(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }

        public void ChangeColor(Color color)
        {
            Color = color;
            _renderer.material.color = color;
        }

        public void MoveTo(Vector2 position)
        {
            transform.DOMove(position, 0.3f);
        }

        public void Disable()
        {
            _renderer.material.DOKill(true);
            transform.DOKill(true);
            Disabled?.Invoke(this);
        }

        public void SetHighlighting()
        {
            Color currentColor = _renderer.material.color;
            Color targetColor = Color.Lerp(currentColor, Color.yellow, 0.5f);   

            _renderer.material.DOColor(targetColor, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        public void SetShacking()
        {
            transform.DOShakePosition(0.3f, strength: 0.1f, vibrato: 5)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
}