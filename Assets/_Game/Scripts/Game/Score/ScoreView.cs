using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        
        public void SetText(string text)
        {
            _scoreText.text = text;
        }
    }
}