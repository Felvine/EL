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
            Vector3 moveDirection = this.Character.Transform.TransformDirection (this.Character.Direction);
            moveDirection *= this.speed;
            this.Character.Controller.Move (moveDirection * Time.deltaTime);
        }
        public override bool IsPrimitive () {
            return true;
        }
    }
}