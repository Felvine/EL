using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class Idle : CharacterAction {
        private float speed;

        public Idle (ControlledAnimatedCharacter characterIn, float durationIn, AnimationClip animationIn) : base (characterIn, durationIn, animationIn) {

        }
        protected override void PerformAction () {

        }
    }
}