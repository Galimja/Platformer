using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Transform _muzzleT;
        public List<PlayerBulletView> _bullets;

        public Action TakeDamage { get; set; }
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
                    TakeDamage?.Invoke();
                }

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                TakeDamage?.Invoke();
            }
        }
    }
}