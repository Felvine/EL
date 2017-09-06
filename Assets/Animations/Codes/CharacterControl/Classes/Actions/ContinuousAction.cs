using System;
using System.Collections.Generic;
using UnityEngine;

//namespace Actions {
//    abstract class ContinuousAction : CharacterAction {
//        private string animatorBoolParameterName; 

//        public ContinuousAction (Character characterIn, float durationIn, string animatorParameterIn) : base (characterIn, durationIn) {
//            animatorBoolParameterName = animatorParameterIn;
//        }
//        protected override void PreActions (ICharacterAction previousAction) {
//            base.PreActions (previousAction);
//            User.Animator.SetBool (animatorBoolParameterName, true);
//        }
//        protected override void PostActions (ICharacterAction nextAction) {
//            Debug.Log (nextAction);
//            base.PostActions (nextAction);
//            if (nextAction != null && nextAction is ContinuousAction) {
//                User.Animator.SetBool (((ContinuousAction)nextAction).animatorBoolParameterName, true);
//            }
//            if (nextAction == null || !(nextAction is ContinuousAction) || ((ContinuousAction)nextAction).animatorBoolParameterName != this.animatorBoolParameterName) {
//                User.Animator.SetBool (animatorBoolParameterName, false);
//            }
//        }

//    }
//}
