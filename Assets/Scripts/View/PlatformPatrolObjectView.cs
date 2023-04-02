using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlatformPatrolObjectView : LevelObjectView
    {
        public Transform _groundDetector;

        public Action<PlayerBulletView> TakeDamage { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out LevelObjectView contactView))
            {
                if (contactView is PlayerBulletView)
                {
                    TakeDamage?.Invoke((PlayerBulletView)contactView);
                }
            }
        }
    }
}