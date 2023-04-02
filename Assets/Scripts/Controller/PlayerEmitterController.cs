using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public class PlayerEmitterController
    {
        private List<PlayerBulletController> _bulletControllers = new List<PlayerBulletController>();
        private Transform _tr;

        public bool _isShoot;
        private int _index;
        private float _xAxisInput;
        private float _prevXAxisInput;
        private float _timeTillNextBullet;
        private float _startSpeed = 35;
        private float _delay = 0.5f;

        public float XAxisInput { get => _xAxisInput; set => _xAxisInput = value; }

        public PlayerEmitterController(List<PlayerBulletView> bulletViews, Transform emitterTransform)
        {
            _tr = emitterTransform;
            foreach (PlayerBulletView bulletView in bulletViews)
            {
                _bulletControllers.Add(new PlayerBulletController(bulletView));
            }
        }

        private Vector3 Direction(float val)
        {
            if (val < 0)
            {
                return -_tr.right;
            } 
            else if (val > 0.01f)
            {
                return _tr.right;
            }
            return (_prevXAxisInput < 0 ? -_tr.right : _tr.right);
        }

        public void Update()
        {
            if (_xAxisInput != 0)
            {
                _prevXAxisInput = _xAxisInput;
            }
                
            if (_timeTillNextBullet > 0)
            {
                _bulletControllers[_index].Active(false);
                
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                if (_isShoot)
                {
                    _timeTillNextBullet = _delay;
                    _bulletControllers[_index].Trow(_tr.position, Direction(_xAxisInput) * _startSpeed);
                    _index++;
                    if (_index >= _bulletControllers.Count)
                    {
                        _index = 0;
                    }
                }
            }
        }
    }
}