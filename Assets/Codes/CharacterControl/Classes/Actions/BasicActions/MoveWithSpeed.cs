using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class MoveWithSpeed : CharacterAction {
        private float speed;

        public MoveWithSpeed (ControlledCharacter characterIn, float durationIn, AnimationClip animationIn, float speedIn) : base (characterIn, durationIn, animationIn) {
            this.speed = speedIn;
            this.priority = 0;
        }
        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection *= this.speed;
            ((ControlledCharacter)this.User).Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}