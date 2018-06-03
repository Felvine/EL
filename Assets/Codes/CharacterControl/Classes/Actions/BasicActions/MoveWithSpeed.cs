using UnityEngine;
using Znko.Events;
using Znko.Characters;

namespace Znko.Actions {
    class MoveWithSpeed : CharacterAction {
        private float speed;

        public MoveWithSpeed (Character characterIn, float durationIn, AnimationClip animationIn, float speedIn, int prioIn, ResourceCost cost = null, params ActionEvent[] events) : base (characterIn, durationIn, animationIn, cost, events) {
            this.speed = speedIn;
            this.priority = prioIn;
        }
        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection = moveDirection.normalized;
            moveDirection *= this.speed;
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}