using System;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public class CoinView : LevelObjectView
    {
        public int point = 1;

        public Action OnCollect { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                OnCollect?.Invoke();
            }
        }


    }
}