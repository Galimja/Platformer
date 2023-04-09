using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlatformerMVC
{
    public class GameOverMenuView : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;

        public void Init(UnityAction reset)
        {
            _resetButton.onClick.AddListener(reset);
        }

        private void OnDisable()
        {
            _resetButton.onClick?.RemoveAllListeners();
        }

    }
}