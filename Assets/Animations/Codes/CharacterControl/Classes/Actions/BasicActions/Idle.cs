using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class Idle : AnimatedAction {
        private float speed;

        public Idle (ControlledCharacter characterIn, float durationIn, AnimationClip animationIn) : base (characterIn, durationIn, animationIn) {

        }
        protected override void PerformAction () {

        }
    }
}