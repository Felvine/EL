using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class Move : Action {
        private float speed;

        public Move (Character characterIn, float durationIn, float speedIn) : base (characterIn, durationIn) {
            this.speed = speedIn;
        }
        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection *= this.speed;
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }
        public override bool IsPrimitive () {
            return true;
        }
    }
}