using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        public Color _completedColor;
        public Color _defaultColor;
        public int id;

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
        }

        public void ProcessComplet()
        {
            _spriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            _spriteRenderer.color = _defaultColor;
        }
    }
}