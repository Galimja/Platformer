using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class OctopusController
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _octopusAnimator;
        private InteractiveEnemyObjectview _octopusObjectView;
        private DeadController _deadController;


        private float _health = 30f;
        private float _animationSpeed = 10f;
        private float _deadTime = 0.6f;
        
        private bool _dead = false;
        

        public OctopusController(InteractiveEnemyObjectview view)
        {
            _octopusObjectView = view;
            _config = Resources.Load<AnimationConfig>("OctopusSpriteAnimCfg");
            _octopusAnimator = new SpriteAnimatorController(_config);
            _octopusAnimator.StartAnimation(_octopusObjectView._spriteRenderer, AnimState.octopus, true, _animationSpeed);

            _octopusObjectView.TakeDamage += TakeBullet;

            _deadController = new DeadController(_octopusObjectView);

        }

        public void TakeBullet(PlayerBulletView bullet)
        {
            _health -= bullet.DamagePoint;
            bullet.gameObject.SetActive(false);
            if (_health < 0)
            {
                _octopusAnimator.Dispose();
                _octopusObjectView._collider.enabled = false;
                _octopusObjectView._rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        public void Update()
        {
            if (_health <= 0)
            {
                _dead = true;
                _deadTime -= Time.deltaTime;
                if (_deadTime < 0)
                    _octopusObjectView.gameObject.SetActive(false);
                //Debug.Log("octopus dead");
                
                _deadController.Update();
            }
            if (!_dead)
                _octopusAnimator.Update();
        }
    }
}