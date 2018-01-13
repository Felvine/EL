using Assets.Codes.CharacterControl.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Actions
{
    class AdvancedAttack : Attack
    {
        private List<string> attackingTypes;
        public AdvancedAttack(Character characterIn, float durationIn, AnimationClip animationClipIn, List<string> weaponTypes) : base(characterIn, durationIn, animationClipIn)
        {
            attackingTypes = weaponTypes;
        }

        public override void PreActions(ICharacterAction previousAction, ICharacterController controller)
        {
            base.PreActions(previousAction, controller);
            this.User.Properties.SetCollidersToAttack(attackingTypes, true);
        }

        protected override void PerformAction()
        {

        }

        public override void PostActions(ICharacterAction nextAction, ICharacterController controller)
        {
            base.PostActions(nextAction, controller);
            this.User.Properties.SetCollidersToAttack(attackingTypes, true);
        }
    }
}
