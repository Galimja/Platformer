
using System.Threading;
using UnityEngine;

namespace PlatformerMVC
{
    public class BulletController
    {
        private Vector3 _velocity;
        private BulletView _view;
        private AnimationConfig _config;
        private SpriteAnimatorController _bulletAnimator;

        


        public BulletController(BulletView bulletView) 
        { 
            _view = bulletView;
            //_config = Resources.Load<AnimationConfig>("BulletSpriteAnimCfg");
            //_bulletAnimator = new SpriteAnimatorController(_config);
            //_bulletAnimator.StartAnimation(_view._spriteRenderer, AnimState.bullet, false, 1f);
            _view.BulletHit += Hit;

            Active(false);
        }

        public void Hit()
        {
            
            //_bulletAnimator.Update();
            //_view._rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Active(false);
        }

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

            _view._rb.AddForce(velocity, ForceMode2D.Impulse);
        }

        
    }
}