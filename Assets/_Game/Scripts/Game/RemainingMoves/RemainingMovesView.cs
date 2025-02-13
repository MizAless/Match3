using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game.RemainingMoves
{
    public class RemainingMovesView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        public void SetText(string text)
        {
            _text.text = text;   
        }
    }
}