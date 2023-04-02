using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public class PlayerBulletController
    {
        private Vector3 _velocity;
        private PlayerBulletView _view;
        private float _speed = 35f;

        public PlayerBulletController(PlayerBulletView bulletView)
        {
            _view = bulletView;
            //_view.BulletHit += Hit;

            Active(false);
        }

        //public void Hit()
        //{

        //    //_bulletAnimator.Update();
        //    //_view._rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //    Active(false);
        //}

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            float _angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 _axis = Vector3.Cross(Vector3.left, _velocity);
            _view._transform.rotation = Quaternion.AngleAxis(_angle, _axis);

        }

        public void Trow(Vector3 position, Vector3 velocity)
        {
            _view._transform.position = position;
            SetVelocity(velocity);
            _view._rb.velocity = Vector2.zero;
            _view._rb.angularVelocity = 0;
            Active(true);
            //_view._rb.velocity = _view._transform.right * _speed;
            _view._rb.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}