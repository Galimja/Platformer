using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformerMVC
{
    public class PortalController
    {
        private const int NeedToWinCoins = 6;
        
        private AnimationConfig _config;
        private SpriteAnimatorController _animController;
        private PortalView _portalView;
        private CoinsCollectView _coinsCollectView;

        private GameWinMenuView _gameWinMenuView;

        private float _animSpeed = 6f;

        public PortalController(PortalView portal, CoinsCollectView coinsCollectView, GameWinMenuView gameWinMenuView)
        {
            _portalView = portal;
            _coinsCollectView = coinsCollectView;
            _gameWinMenuView = gameWinMenuView;

            _config = Resources.Load<AnimationConfig>("PortalAnimCfg");
            _animController = new SpriteAnimatorController(_config);

            _animController.StartAnimation(_portalView._spriteRenderer, AnimState.portal, true, _animSpeed);

            _portalView.EnterToPortal += EnterToPortal;
            _gameWinMenuView.Init(ResetLevel);
        }

        private void EnterToPortal()
        {
            Debug.Log($"Your score {_coinsCollectView.pointCount}");
            if (_coinsCollectView.pointCount >= NeedToWinCoins)
            {
                GameWin();
            }
        }

        public void Update()
        {
            _animController.Update();
        }

        private void GameWin()
        {
            Time.timeScale = 0f;
            _gameWinMenuView.TextReset(_coinsCollectView.pointCount);
            _gameWinMenuView.gameObject.SetActive(true);
        }

        private void ResetLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}