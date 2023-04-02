using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class DeadController
    {
        private LevelObjectView _view;
        private SpriteAnimatorController _animController;
        private AnimationConfig _config;

        private float _animationSpeed = 10f;

        public DeadController(LevelObjectView view)
        {
            _view = view;
            _config = Resources.Load<AnimationConfig>("DeadAnimation");
            _animController = new SpriteAnimatorController(_config);
        }

        public void Update()
        {
            _animController.StartAnimation(_view._spriteRenderer, AnimState.dead, false, _animationSpeed);
            _animController.Update();
        }
    }
}