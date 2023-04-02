using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EmitterController
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _tr;

        private int _index;
        private float _timeTillNextBullet;
        private float _startSpeed = 25;
        private float _delay = 5;

        public EmitterController(List<BulletView> bulletViews, Transform emitterTransform)
        {
            _tr = emitterTransform;
            foreach (BulletView bulletView in bulletViews)
            {
                _bulletControllers.Add(new BulletController(bulletView));
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bulletControllers[_index].Trow(_tr.position, -_tr.up * _startSpeed);
                _index++;
                if (_index >= _bulletControllers.Count)
                {
                    _index = 0;
                }
            }
        }

    }
}