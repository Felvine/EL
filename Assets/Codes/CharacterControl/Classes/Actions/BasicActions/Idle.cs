using UnityEngine;
using Znko.Events;
using Znko.Characters;

namespace Znko.Actions {
    class Idle : CharacterAction {

        public Idle (Character characterIn, float durationIn, AnimationClip animationIn, int priorityIn, params ActionEvent[] events) : base (characterIn, durationIn, animationIn, events) {
            this.priority = priorityIn;
        }
        protected override void PerformAction () {

        }
    }
}