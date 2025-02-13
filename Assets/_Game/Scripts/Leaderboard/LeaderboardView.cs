using UnityEngine;

namespace _Game.Scripts.Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        
        public void AddElement(RowView rowView)
        {
            rowView.transform.SetParent(_content);
        }
        
        public void Highlight(RowView rowView)
        {
            rowView.SetHighlighting();
        }
    }
}