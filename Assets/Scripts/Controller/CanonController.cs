using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CanonController
    {
        private Transform _muzzleT;
        private Transform _targetT;

        private Vector3 _dir;
        private Vector3 _axis;

        private float _angle;

        public CanonController(Transform muzzle, Transform target)
        {
            _muzzleT = muzzle;
            _targetT = target;
        }

        
        public void Update()
        {
            _dir = _targetT.position - _muzzleT.position;
            _angle = Vector3.Angle(Vector3.down, _dir);
            _axis = Vector3.Cross(Vector3.down, _dir);

            _muzzleT.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}