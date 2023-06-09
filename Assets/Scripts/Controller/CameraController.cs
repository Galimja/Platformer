using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CameraController
    {
        private LevelObjectView _player;

        private Transform _playerT;
        private Transform _cameraT;

        private float _cameraSpeed = 5.2f;

        private float X;
        private float Y;

        private float offsetY;
        private float offsetX;

        private float _xAxisInput;
        private float _yAxisInput;

        private float _treshold;

        private float _leftBarrier = -40f;
        private float _downBarrier = -100f;

        public CameraController(LevelObjectView player, Transform camera)
        {
            _player = player;
            _cameraT = camera;
            _playerT = player._transform;
            _treshold = 0.2f;
        }

        public void Update()
        {
            /*_xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = _player._rb.velocity.y;

            X = _playerT.position.x;
            Y = _playerT.position.y;

            if (_xAxisInput > _treshold)
            {
                offsetX = 4;
            } else if (_xAxisInput < -_treshold)
            {
                offsetX = -4;
            } else
            {
                offsetX = 0;
            }

            if (_yAxisInput > _treshold)
            {
                offsetY = 4;
            }
            else if (_yAxisInput < -_treshold)
            {
                offsetY = -4;
            }
            else
            {
                offsetY = 0;
            }

            _cameraT.position = Vector3.MoveTowards(_cameraT.position, 
                new Vector3(X + offsetX, Y + offsetY, _cameraT.position.z), Time.deltaTime * _cameraSpeed);
            */


            _cameraT.position = new Vector3(_playerT.position.x, _playerT.position.y, _cameraT.position.z);
            if (_playerT.position.x < _leftBarrier)
            {
                _cameraT.position = new Vector3(_leftBarrier, _playerT.position.y, _cameraT.position.z);
            }
            if (_playerT.position.y < _downBarrier)
            {
                _cameraT.position = new Vector3(_playerT.position.x, _downBarrier, _cameraT.position.z);
            }

        }
    }
}