using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Leaderboard
{
    public class RowView : MonoBehaviour
    {
        [SerializeField] private Text _scoreCell;
        [SerializeField] private Text _dateCell;
        [SerializeField] private Image _image;
        
        public void SetData(string score, string date)
        {
            _scoreCell.text = score;    
            _dateCell.text = date;    
        }
        
        public void SetHighlighting()
        {
            _image.DOFade(0.2f, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}