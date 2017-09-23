using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions {
    class Attack : CharacterAction {
        public Attack (Character characterIn, float durationIn, AnimationClip animationClipIn) : base (characterIn, durationIn, animationClipIn) {
			this.priority = 1;
        }

        public override void PreActions (ICharacterAction previousAction) {
            base.PreActions (previousAction);
            this.User.Properties.IsAttacking = true;
        }

        protected override void PerformAction () {

        }

        public override void PostActions (ICharacterAction nextAction) {
            base.PostActions (nextAction);
            this.User.Properties.IsAttacking = false;
        }
    }
}
