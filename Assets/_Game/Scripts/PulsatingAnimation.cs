using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts
{
    public class PulsatingAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField, Range(0.01f, 0.99f)] private float _offset;

        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale( 1 + _offset, _duration).SetEase(Ease.Linear));
            sequence.Append(transform.DOScale( 1, _duration).SetEase(Ease.Linear));
            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}