using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerMVC
{
    public class CrabController : IEnemy
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _crabAnimator;
        private PlatformPatrolObjectView _crabView;
        private DeadController _deadController;
        
        private Transform _playerT;
        private Transform _goundDetect;
        private Rigidbody2D _rb;
        

        private float _health = 50f;
        private float _moveSpeed = 50f;
        private float _animationSpeed = 10f;
        private float _distance = 1f;
        private float _deadTime = 0.6f;

        private bool _dead = false;
        private bool _leftMove = true;


        public CrabController(PlatformPatrolObjectView crab, Transform playerT)
        {
            _crabView = crab;
            _playerT = playerT;
            _config = Resources.Load<AnimationConfig>("CrabSpriteAnimCfg");
            _crabAnimator = new SpriteAnimatorController(_config);
            _goundDetect = crab._groundDetector;
            _rb = crab._rb;

            _crabAnimator.StartAnimation(_crabView._spriteRenderer, AnimState.crabWalk, true, _animationSpeed);

            _crabView.TakeDamage += TakeBullet;

            _deadController = new DeadController(_crabView);
        }

        public void TakeBullet(PlayerBulletView bullet)
        {
            _health -= bullet.DamagePoint;
            bullet.gameObject.SetActive(false);
            if (_health < 0)
            {
                _crabAnimator.Dispose();
                _crabView._collider.enabled = false;
                _crabView._rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        public void Update()
        {
            if (_health <= 0)
            {
                _dead = true;
                _deadTime -= Time.deltaTime;
                if (_deadTime < 0)
                    _crabView.gameObject.SetActive(false);

                _deadController.Update();
            }
            if (!_dead)
                _crabAnimator.Update();

            _rb.velocity = new Vector2(Time.fixedDeltaTime * _moveSpeed * (_leftMove ? -1 : 1), _rb.velocity.y);
            

            RaycastHit2D groundInfo = Physics2D.Raycast(_goundDetect.position, Vector2.down, _distance);
            
            if (groundInfo.collider == false)
            {
                FlipOrientation();
            } else
            {
                if (groundInfo.collider.tag != "PatrollPlatform")
                    FlipOrientation();
            }
        }

        private void FlipOrientation()
        {
            if (_leftMove)
            {
                _crabView._transform.eulerAngles = new Vector3(0, -180, 0);
                _leftMove = false;
            }
            else
            {
                _crabView._transform.eulerAngles = new Vector3(0, 0, 0);
                _leftMove = true;
            }
        }
    }
}