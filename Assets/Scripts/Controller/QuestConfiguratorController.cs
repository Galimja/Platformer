using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestConfiguratorController 
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuestController;
        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _storyQuestViews;
        private QuestCoinModel _questCoinModel;

        private List<IQuestStory> _questStoryList;
        
        private InteractiveObjectView _player;

        private Dictionary<QuestType, Func<IQuestModel>> _questFactory = 
                                new Dictionary<QuestType, Func<IQuestModel>>(10);
        private Dictionary<StoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactory = 
                                new Dictionary<StoryType, Func<List<IQuest>, IQuestStory>>(10);
        
        public QuestConfiguratorController(QuestView questView, InteractiveObjectView player)
        {
            _singleQuestView = questView._singleQuest;
            _storyQuestViews = questView._questObjects;
            _questCoinModel = new QuestCoinModel();
            _questStoryConfigs = questView._storyConfig;

            _player = player;


        }

        public void Start()
        {
            _singleQuestController = new QuestController(_player, _singleQuestView, _questCoinModel);
            _singleQuestController.Reset();

            _questFactory.Add(QuestType.Coins, () => new QuestCoinModel());
            _questStoryFactory.Add(StoryType.Common, questCollection => new QuestStoryController(questCollection));

            _questStoryList = new List<IQuestStory>();

            foreach (QuestStoryConfig cfg in _questStoryConfigs)
            {
                _questStoryList.Add(CreateQuestStory(cfg));
            }
        }

        private IQuest CreateQuest(QuestConfig cfg)
        {
            int questId = cfg.id;
            QuestObjectView qView = _storyQuestViews.FirstOrDefault(value => value.id == questId);

            if (qView == null)
            {
                Debug.Log("No View");
                return null;
            }

            if (_questFactory.TryGetValue(cfg.questType, out var factory))
            {
                IQuestModel qModel = factory.Invoke();
                return new QuestController(_player, qView, qModel);
            }

            Debug.Log("No Model");
            return null;
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig item in cfg.questsConfig)
            {
                IQuest quest = CreateQuest(item);
                if (quest == null)
                {
                    continue;
                }
                quests.Add(quest);
                Debug.Log("AddQuest");
            }

            return _questStoryFactory[cfg.type].Invoke(quests);

        }
       
    }
}