using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformerMVC
{
    public class PlayerController : IDisposable
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimator;
        private ContactPooler _contactPooler;

        private InteractiveObjectView _playerView;
        private PlayerUIView _playerUIView;
        private GameOverMenuView _gameOverMenuView;

        private PlayerEmitterController _playerEmitter;
        
        private Transform _playerT;
        private Rigidbody2D _rb;

        private int _health = 5;
        
        private bool _isDead = false;
        private bool _isJump;
        private bool _isMoving;
        private bool _isDuck;
        private bool _isHurt = false;
        public bool _isShoot;

        private float _bulletForce = 3f;
        private float _walkSpeed = 180f;
        private float _animationSpeed = 16f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

       
        private float _jumpForce = 9f;
        private float _jumpTreshold = 1f;
        
        private float _xAxisInput;
        private float _yVelocity = 0;
        private float _xVelocity = 0;

        private float _hurtTime;
        private float _hurtDelay = 1.5f;

        public PlayerController(InteractiveObjectView player, PlayerUIView uiView, GameOverMenuView gameOverView)
        {
            _playerView = player;
            _playerUIView = uiView;
            _playerT = player._transform;
            _rb = player._rb;

            _gameOverMenuView = gameOverView;
            _gameOverMenuView.Init(ResetLevel);

            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.Run, true, 10f);
            _contactPooler = new ContactPooler(player._collider);

            _playerEmitter = new PlayerEmitterController(player._bullets, player._muzzleT);

            _playerView.TakeDamage += TakeDamage;
        }

        private void ResetLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void TakeDamage()
        {
            if (_hurtTime < 0.02f)
            {
                _health--;
                _isHurt = true;
                _playerUIView._heartImages[_health].SetActive(false);
                Debug.Log(_health);
            }

            //_playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.hurt, true, _animationSpeed / 10);
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public void Update()
        {
            if (_health <= 0)
            {
                _health = 0;

                if (!_isDead)
                    GameOver();
            }

            if (_playerT.position.y < -25f)
            {
                if (!_isDead)
                    GameOver();
            }

            if (_isHurt) { 
                _hurtTime += Time.deltaTime;
                if (_hurtTime - _hurtDelay > 0f)
                {
                    _isHurt = false;
                    _hurtTime = 0f;
                }
            }

            _playerAnimator.Update();
            _contactPooler.Update();
            
            _xAxisInput = Input.GetAxis("Horizontal");
            _playerEmitter.XAxisInput = _xAxisInput;
            _isJump = Input.GetAxis("Vertical") > 0;
            _playerEmitter._isShoot = Input.GetKeyDown(KeyCode.Mouse0) && _contactPooler.IsGrounded;
            //_isDuck = Input.GetAxis("Vertical") < 0;
            _yVelocity = _rb.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            _playerEmitter.Update();



            if (_isMoving /*&& !_isDuck*/)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            }

            if (_contactPooler.IsGrounded)
            {
                //if (_isShoot)
                //{
                //    //_playerAnimator.StartAnimation(_playerView._spriteRenderer,
                //    //    _isMoving ? AnimState.shoot : AnimState.shoot_stand, true, _animationSpeed);
                //}
                //else
                //{
                //    _playerAnimator.StartAnimation(_playerView._spriteRenderer,
                //    _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);
                //}
                _playerAnimator.StartAnimation(_playerView._spriteRenderer,
                    _isMoving ? AnimState.shoot : AnimState.shoot_stand, true, _animationSpeed);



                //if (_isDuck)
                //{
                //    _xVelocity = 0;
                //    _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
                //    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.duck, false, _animationSpeed * 3);
                //}

                if (_isHurt)
                {
                    //_playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.hurt, true, _animationSpeed / 10000);
                    //_rb.AddForce(Vector2.left * (_xAxisInput < 0 ? -1 : 1) * _bulletForce, ForceMode2D.Impulse);
                    _isHurt = false;
                }

                if (_isJump && _yVelocity <= _jumpTreshold)
                {
                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                if (Mathf.Abs(_rb.velocity.y) > _jumpTreshold)
                {
                    //Debug.Log("Jump start");
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.jump, true, _animationSpeed);
                }
            }

            
        }

        private void GameOver()
        {
            _isDead = true;
            _playerView._spriteRenderer.enabled = false;
            Time.timeScale = 0f;
            _gameOverMenuView.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            ((IDisposable)_playerAnimator).Dispose();
            _playerView.TakeDamage -= TakeDamage;
        }
    }
}