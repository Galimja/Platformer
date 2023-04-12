using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace PlatformerMVC
{
    public class GameWinMenuView : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;
        [SerializeField] private TMP_Text _text;

        public void Init(UnityAction reset)
        {
            _resetButton.onClick.AddListener(reset);
        }

        public void TextReset(int point)
        {
            _text.text = $"Score: {point}!";
        }

        private void OnDisable()
        {
            _resetButton.onClick?.RemoveAllListeners();
        }

    }
}