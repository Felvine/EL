using System;
using System.Collections.Generic;
using UnityEngine;
namespace Actions {
    class MoveToDistance : CharacterAction {
        private bool invincibility;
        private float speed;
        public MoveToDistance (ControlledAnimatedCharacter characterIn, float durationIn, AnimationClip animationIn, float distanceIn, bool invicibilityIn) : base (characterIn, durationIn, animationIn) {
            this.invincibility = invicibilityIn;
            this.speed = distanceIn / durationIn;
            this.priority = 1;
        }

        public override void PreActions (ICharacterAction previousAction) {
            base.PreActions (previousAction);
            if (invincibility) {
                SpriteRenderer[] spriteRenderers = User.Transform.GetComponentsInChildren<SpriteRenderer> ();
                foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                    spriteRenderer.color = Color.red;
                }
            }
        }

        public override void PostActions (ICharacterAction nextAction) {
            base.PostActions (nextAction);
            if (invincibility) {
                SpriteRenderer[] spriteRenderers = User.Transform.GetComponentsInChildren<SpriteRenderer> ();
                foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                    spriteRenderer.color = Color.white;
                }
            }
        }

        protected override void PerformAction () {
            Vector3 moveDirection = this.User.Transform.TransformDirection (this.User.Direction);
            moveDirection *= this.speed;
            ((ControlledAnimatedCharacter)this.User).Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}