using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{

    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CanonView _canonView;
        [SerializeField] private LevelObjectView _cameraView;
        [SerializeField] private LevelObjectView[] _enemiesView;
        [SerializeField] private GeneratorLevelView _generatorLevelView;
        [SerializeField] private QuestView _questView;
        [SerializeField] private CoinsCollectView _coinsCollectView;
        [SerializeField] private CaveZoneView _caveZoneView;
        [SerializeField] private PortalView _portalView;

        [SerializeField] private PauseMenuView _pauseMenuView;
        [SerializeField] private PlayerUIView _playerUIView;
        [SerializeField] private GameOverMenuView _gameOverMenuView;
        [SerializeField] private GameWinMenuView _gameWinMenuView;
        //[SerializeField] private QuestObjectView _singleQuestItemView; 


        private PlayerController _playerController;
        private CanonController _canonController;
        private EmitterController _emitterController;
        private CameraController _cameraController;
        private GeneratorController _generatorController;
        private QuestConfiguratorController _questConfiguratorController;
        private CoinCollectController _collectController;
        private CaveZoneController _caveZoneController;
        private PortalController _portalController;

        private PauseMenuController _pauseMenuController;

        private List<OctopusController> _octopusController = new List<OctopusController>();
        private List<CrabController> _crabController = new List<CrabController>();

        //private QuestController _questController;

        private void Awake()
        {

            _playerController = new PlayerController(_playerView, _playerUIView, _gameOverMenuView);
            _canonController = new CanonController(_canonView._muzzleT, _playerView._transform);
            _emitterController = new EmitterController(_canonView._bullets ,_canonView._emitterT);
            _cameraController = new CameraController(_playerView, _cameraView._transform);
            _pauseMenuController = new PauseMenuController(_pauseMenuView);
            _portalController = new PortalController(_portalView, _coinsCollectView, _gameWinMenuView);

            for (int i = 0; i < _enemiesView.Length; i++)
            {
                if (_enemiesView[i] is InteractiveEnemyObjectview)
                {
                    _octopusController.Add(new OctopusController((InteractiveEnemyObjectview)_enemiesView[i]));
                }
                else if (_enemiesView[i] is PlatformPatrolObjectView)
                {
                    _crabController
                    .Add(new CrabController((PlatformPatrolObjectView)_enemiesView[i], _playerView._transform));
                }
            }

            //_generatorController = new GeneratorController(_generatorLevelView);
            //_generatorController.Start();

            _collectController = new CoinCollectController(_coinsCollectView);
            _caveZoneController = new CaveZoneController(_caveZoneView);

            _questConfiguratorController = new QuestConfiguratorController(_questView, _playerView);
            _questConfiguratorController.Start();
        }


        void Update()
        {
            _playerController.Update();
            _canonController.Update();
            _emitterController.Update();
            _cameraController.Update();
            _collectController.Update();
            _portalController.Update();

            for (int i = 0; i < _octopusController.Count; i++)
            {
                _octopusController[i].Update();
            }

            for (int i = 0; i < _crabController.Count; i++)
            {
                _crabController[i].Update();
            }

        }
    }
}