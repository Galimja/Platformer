using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerBulletView : LevelObjectView
    {
        private int _damagePoint = 20;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }
    }
}