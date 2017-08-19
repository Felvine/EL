using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class Move : CharacterAction {
        private float speed;
        string animatonBool;

        public Move (Character characterIn, float durationIn, float speedIn, string animatonBool) : base (characterIn, durationIn) {
            this.animatonBool = animatonBool;
            this.speed = speedIn;
        }
        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection *= this.speed;
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }

        protected override void PreActions () {
            base.PreActions ();
            User.Animations.SetBool (animatonBool, true);
        }
        public override bool IsPrimitive () {
            return true;
        }
    }
}