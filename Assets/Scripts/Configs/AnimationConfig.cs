using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{

    public enum AnimState
    {
        Idle = 0,
        Run = 1,
        jump = 2,
        duck = 3,
        hurt = 4,
        octopus = 5,
        crabIdle = 6,
        crabWalk = 7,
        bullet = 8,
        shoot = 9,
        shoot_stand = 10,
        dead = 11,
        Coin = 12,
        portal = 13
    }

    [CreateAssetMenu(fileName ="SpriteAnimatorCfg", menuName ="Configs / Animation", order = 1)]
    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public AnimState Track;
            public List<Sprite> Sprites = new List<Sprite> ();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence> ();
    }
}