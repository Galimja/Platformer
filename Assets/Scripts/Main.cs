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
        [SerializeField] private InteractiveEnemyObjectview _octopusView;
        [SerializeField] private PlatformPatrolObjectView _crabView;
        [SerializeField] private GeneratorLevelView _generatorLevelView;
        [SerializeField] private QuestView _questView;
        [SerializeField] private CoinsCollectView _coinsCollectView;
        [SerializeField] private CaveZoneView _caveZoneView;
        //[SerializeField] private QuestObjectView _singleQuestItemView; 


        private PlayerController _playerController;
        private CanonController _canonController;
        private EmitterController _emitterController;
        private CameraController _cameraController;
        private OctopusController _octopusController;
        private CrabController _crabController;
        private GeneratorController _generatorController;
        private QuestConfiguratorController _questConfiguratorController;
        private CoinCollectController _collectController;
        private CaveZoneController _caveZoneController;
        //private QuestController _questController;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _canonController = new CanonController(_canonView._muzzleT, _playerView._transform);
            _emitterController = new EmitterController(_canonView._bullets ,_canonView._emitterT);
            _cameraController = new CameraController(_playerView, _cameraView._transform);
            _octopusController = new OctopusController(_octopusView);
            _crabController = new CrabController(_crabView, _playerView._transform);
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
            _octopusController.Update();
            _crabController.Update();
            _collectController.Update();
        }
    }
}