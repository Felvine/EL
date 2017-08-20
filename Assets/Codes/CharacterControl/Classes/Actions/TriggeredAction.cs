using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Actions {
   abstract class TriggeredAction : CharacterAction {
        private const float almostDone = 0.95f;
        private string animatorTriggerParameterName;


        public TriggeredAction (Character characterIn, float durationIn, string triggerIn) : base (characterIn, durationIn) {
            this.animatorTriggerParameterName = triggerIn;
        }

        public override void PreActions (ICharacterAction previousAction) {
            base.PreActions (previousAction);
            User.Animator.SetTrigger (animatorTriggerParameterName);
        }
        public virtual bool AlmostDone () {
            return (StartTime + (Duration * almostDone) < Time.time);
        }
    }
}
