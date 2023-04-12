using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class PortalView : LevelObjectView
    {

        public Action EnterToPortal { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                EnterToPortal?.Invoke();
            }
        }

    }
}