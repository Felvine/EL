using System;
using System.Collections.Generic;
using UnityEngine;
namespace Actions {
    class Roll : TriggeredAction {
        private bool invincibility;
        private float speed;
        public Roll (Character characterIn, float durationIn, float distanceIn, string triggerIn, bool invicibilityIn) : base (characterIn, durationIn, triggerIn) {
            this.invincibility = invicibilityIn;
            this.speed = distanceIn / durationIn;
        }

        protected override void PreActions (ICharacterAction previousAction) {
            base.PreActions (previousAction);
            if (invincibility) {
                SpriteRenderer[] spriteRenderers = User.Transform.GetComponentsInChildren<SpriteRenderer> ();
                foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                    spriteRenderer.color = Color.red;
                }
            }
        }

        protected override void PostActions (ICharacterAction nextAction) {
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
            this.User.Controller.Move (moveDirection * Time.deltaTime);
        }
    }
}