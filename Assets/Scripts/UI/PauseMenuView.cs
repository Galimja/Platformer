using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlatformerMVC
{
    public class PauseMenuView : MonoBehaviour
    {
        

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _resetButton;
        
        [SerializeField] private GameObject _pausePanel;

        
        public GameObject PausePanel { get => _pausePanel; }


        public void Init(UnityAction pause, UnityAction back, UnityAction reset)
        {
            _pauseButton.onClick.AddListener(pause);
            _backButton.onClick.AddListener(back);
            _resetButton.onClick.AddListener(reset);
        }

        

        private void OnDisable()
        {
            _pauseButton.onClick?.RemoveAllListeners();
            _backButton.onClick?.RemoveAllListeners();
            _resetButton.onClick?.RemoveAllListeners();
        }

    }
}