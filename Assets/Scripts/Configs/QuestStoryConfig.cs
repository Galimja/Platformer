using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public enum StoryType
    {
        Common,
        Resetteble
    }

    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / QuestSystem / QuestStoryCfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] questsConfig;
        public StoryType type;


    }
}