using UnityEngine;
using Znko.Events;
using Znko.Characters;

namespace Znko.Actions {
    class Idle : CharacterAction {

        public Idle (Character characterIn, float durationIn, AnimationClip animationIn, int priorityIn, ResourceCost cost = null, params ActionEvent[] events) : base (characterIn, durationIn, animationIn, cost, events) {
            this._priority = priorityIn;
        }
        protected override void PerformAction () {

        }
    }
}