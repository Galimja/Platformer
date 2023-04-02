using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace PlatformerMVC
{
    public class CaveZoneView : LevelObjectView
    {
        [SerializeField] public Tilemap _hideZone;

        public Action OpenZone { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                OpenZone?.Invoke();
            }
        }
    }
}