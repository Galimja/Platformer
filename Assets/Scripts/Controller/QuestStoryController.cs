using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestStoryController : IQuestStory
    {
        private List<IQuest> _questCollection = new List<IQuest>();

        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        public QuestStoryController(List<IQuest> questCollection)
        {
            _questCollection = questCollection;

            foreach (IQuest quest in _questCollection)
            {
                quest.QuestCompleted += OnQuestCompleted;
            }

            Reset(0);
        }

        private void Reset(int index)
        {
            if (index < 0 || index > _questCollection.Count) return;

            IQuest quest = _questCollection[index];

            if (quest.IsCompleted)
            {
                OnQuestCompleted(this, quest);
            }
            else
            {
                quest.Reset();
            }
        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            int index = _questCollection.IndexOf(quest);

            if (IsDone)
            {
                Debug.Log("Story is Done!");
            } else
            {
                Debug.Log("Story reset!");
                Reset(index);
            }
            
        }

        public void Dispose()
        {
            foreach (IQuest quest in _questCollection)
            {
                quest.QuestCompleted -= OnQuestCompleted;
                quest.Dispose();
            }
        }
    }
}