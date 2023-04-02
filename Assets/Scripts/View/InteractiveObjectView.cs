using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Transform _muzzleT;
        public List<PlayerBulletView> _bullets;

        public Action<BulletView> TakeDamage { get; set; }
        public Action<QuestObjectView> OnQuestComplete { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out LevelObjectView contactView))
            {
                if (contactView is QuestObjectView)
                {
                    OnQuestComplete?.Invoke((QuestObjectView)contactView);
                }
                if (contactView is BulletView)
                {
                    TakeDamage?.Invoke((BulletView)contactView);
                }
            }
        }
    }
}