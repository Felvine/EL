using UnityEngine;
using Znko.Events;
using Znko.Characters;

namespace Znko.Actions {
    class MoveToDistance : CharacterAction {
        private float speed;
        public MoveToDistance (Character characterIn, float durationIn, AnimationClip animationIn, float distanceIn, ResourceCost cost = null, params ActionEvent[] events) : base (characterIn, durationIn, animationIn, cost, events) {
            this.speed = distanceIn / durationIn;
            this._priority = 1;
        }

        public override void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            base.PreActions (previousAction, controller);
        }

        public override void PostActions (ICharacterAction nextAction, ICharacterController controller) {
            base.PostActions (nextAction, controller);
        }

        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection = Vector3.Normalize(moveDirection);
            moveDirection *= this.speed;
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}