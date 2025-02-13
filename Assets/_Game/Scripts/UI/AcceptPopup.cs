using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class AcceptPopup : MonoBehaviour
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _declineButton;

        private UnityAction _declineAction;

        public void AddAction(UnityAction acceptAction, UnityAction declineAction)
        {
            _acceptButton.onClick.AddListener(acceptAction);
            _declineAction = declineAction;
            _declineButton.onClick.AddListener(OnDecline);
        }
        
        public void AddAction(UnityAction acceptAction)
        {
            _acceptButton.onClick.AddListener(acceptAction);
            _declineButton.onClick.AddListener(OnDecline);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        private void OnDecline()
        {
            _declineAction?.Invoke();
            _acceptButton.onClick.RemoveAllListeners();
            _declineButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}