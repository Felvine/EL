using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class Move : AnimatedAction {
        private float speed;

        public Move (ControlledCharacter characterIn, float durationIn, AnimationClip animationIn, float speedIn) : base (characterIn, durationIn, animationIn) {
            this.speed = speedIn;
        }
        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection *= this.speed;
            ((ControlledCharacter)this.User).Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}