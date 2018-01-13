using Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    class Stagger : CharacterAction {
        public Stagger(Character characterIn, float durationIn, AnimationClip animationIn, int priority) : base (characterIn, durationIn, animationIn) {
            this.priority = priority;
        }
        protected override void PerformAction()
        {

        }

        public override bool CanInterrupt (ICharacterAction currentAction)
        {
            return (this.priority > currentAction.Priority);
        }
    }
}
