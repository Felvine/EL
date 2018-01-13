using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions {
    class Attack : CharacterAction {

        private bool isHeavy;

        public bool IsHeavy {
            get {
                return isHeavy;
            }

            set {
                isHeavy = value;
            }
        }

        public Attack (Character characterIn, float durationIn, AnimationClip animationClipIn) : base (characterIn, durationIn, animationClipIn) {
			this.priority = 1;
            this.isHeavy = false;
        }

        public override void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            base.PreActions (previousAction, controller);
            this.User.Properties.IsAttacking = true;
        }

        protected override void PerformAction () {

        }

        public override void PostActions (ICharacterAction nextAction, ICharacterController controller) {
            base.PostActions (nextAction, controller);
            this.User.Properties.IsAttacking = false;
        }
    }
}
