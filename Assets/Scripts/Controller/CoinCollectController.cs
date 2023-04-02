using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CoinCollectController
    {
        private CoinsCollectView _coinsCollectView;
        private List<CoinController> _coinsControllers = new List<CoinController>();

        public CoinCollectController(CoinsCollectView coinsCollectView)
        {
            _coinsCollectView = coinsCollectView;
            foreach (CoinView coinView in _coinsCollectView._coins)
            {
                _coinsControllers.Add(new CoinController(coinView, _coinsCollectView));
            }
        }
        
        public void Update()
        {
            foreach (CoinController coinController in _coinsControllers)
            {
                coinController.Update();
            }
        }
    }
}