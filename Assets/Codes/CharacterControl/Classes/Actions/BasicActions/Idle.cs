using UnityEngine;
using Znko.Events;
using Znko.Characters;

namespace Znko.Actions {
    class Idle : CharacterAction {

        public Idle (Character characterIn, float durationIn, AnimationClip animationIn, int priorityIn, ResourceCost cost = null, params ActionEvent[] events) : base (characterIn, durationIn, animationIn, cost, events) {
            this._priority = priorityIn;
            foreach (ActionEvent eH in events)
            {
                switch (eH.Phase)
                {
                    case ActionEvent.ActionPhase.PreAction:
                        this.PreActionEvent += eH.Value;
                        break;
                    case ActionEvent.ActionPhase.PostAction:
                        this.PostActionEvent += eH.Value;
                        break;
                    default:
                        break;
                }
            }
        }
        protected override void PerformAction () {

        }
    }
}