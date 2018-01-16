using UnityEngine;
namespace Znko.Actions {
    class MoveWithSpeed : CharacterAction {
        private float speed;

        public MoveWithSpeed (Character characterIn, float durationIn, AnimationClip animationIn, float speedIn) : base (characterIn, durationIn, animationIn) {
            this.speed = speedIn;
            this.priority = 1;
        }
        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection = moveDirection.normalized;
            moveDirection *= this.speed;
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}