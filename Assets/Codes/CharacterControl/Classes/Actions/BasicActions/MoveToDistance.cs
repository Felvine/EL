using UnityEngine;
using Znko.Events;
using Znko.Characters;

namespace Znko.Actions {
    class MoveToDistance : CharacterAction {
        private float speed;
        public MoveToDistance (Character characterIn, float durationIn, AnimationClip animationIn, float distanceIn, params ActionEvent[] events) : base (characterIn, durationIn, animationIn, events) {
            this.speed = distanceIn / durationIn;
            this.priority = 2;
        }

        public override void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            base.PreActions (previousAction, controller);
        }

        public override void PostActions (ICharacterAction nextAction, ICharacterController controller) {
            base.PostActions (nextAction, controller);
        }

        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection *= this.speed;
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}