using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class BulletView : LevelObjectView
    {
        private int _damagePoint = 10;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }

        public Action BulletHit { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractiveObjectView contactView))
            {
                BulletHit?.Invoke();
            }
        }
    }
}