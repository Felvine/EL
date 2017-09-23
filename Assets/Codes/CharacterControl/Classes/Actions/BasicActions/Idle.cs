using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
    class Idle : CharacterAction {

        public Idle (ControlledCharacter characterIn, float durationIn, AnimationClip animationIn) : base (characterIn, durationIn, animationIn) {
            this.priority = 0;
        }
        protected override void PerformAction () {

        }
    }
}