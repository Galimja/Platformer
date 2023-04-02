using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }
    }
}