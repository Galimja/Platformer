using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestController : IQuest
    {
        private InteractiveObjectView _player;
        private QuestObjectView _quest;

        private bool _active;
        private IQuestModel _model;

        public event EventHandler<IQuest> QuestCompleted;

        public bool IsCompleted { get; private set; }

        public QuestController(InteractiveObjectView player, QuestObjectView view, IQuestModel model)
        {
            _player = player;
            _active = false;
            _model = model;
            _quest = view;
        }

        public void OnContact(QuestObjectView questItem)
        {
            if (questItem != null)
            {
                if (_model.TryComplete(questItem.gameObject))
                {
                    if (questItem == _quest)
                    {
                        Completed();
                    }
                }
            }
        }

        public void Completed()
        {
            if (!_active) return;
            _active = false;
            _player.OnQuestComplete -= OnContact;
            _quest.ProcessComplet();
            QuestCompleted?.Invoke(this, this);

        }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            _player.OnQuestComplete += OnContact;
            _quest.ProcessActivate();

        }
        
        public void Dispose()
        {
            _player.OnQuestComplete -= OnContact;
        }
        
    }
}