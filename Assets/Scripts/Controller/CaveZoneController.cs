using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace PlatformerMVC
{
    public class CaveZoneController
    {
        private CaveZoneView _view;

        public CaveZoneController(CaveZoneView view)
        {
            _view = view;
            _view.OpenZone += ZoneOpen;
        }

        public void ZoneOpen()
        {
            _view._hideZone.gameObject.SetActive(false);
        }
    }
}