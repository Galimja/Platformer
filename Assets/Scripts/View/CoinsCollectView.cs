using System;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public class CoinsCollectView : LevelObjectView
    {
        [SerializeField] public List<CoinView> _coins;

        public int pointCount = 0;

    }
}