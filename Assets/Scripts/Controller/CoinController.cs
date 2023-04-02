using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CoinController : MonoBehaviour
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _animController;
        private CoinView _coinView;
        private CoinsCollectView _coinsCollectView;

        private float _animSpeed = 10f;

        public CoinController(CoinView coinView, CoinsCollectView coinsCollectView)
        {
            _coinView = coinView;
            _coinsCollectView = coinsCollectView;
            _config = Resources.Load<AnimationConfig>("CoinAnimCfg");
            _animController = new SpriteAnimatorController(_config);

            _animController.StartAnimation(_coinView._spriteRenderer, AnimState.Coin, true, _animSpeed);
            
            _coinView.OnCollect += OnCollect;
        }

        public void OnCollect()
        {
            _coinsCollectView.pointCount += _coinView.point;
            _coinView.gameObject.SetActive(false);
        }

        public void Update()
        {
            _animController.Update();
        }
    }
}