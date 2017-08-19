using System;
using System.Collections.Generic;
using UnityEngine;
namespace Actions {
    class Roll : CharacterAction {
        private bool invincibility;
        private float speed;
        public Roll (Character characterIn, float durationIn, float distanceIn, bool invicibilityIn) : base (characterIn, durationIn) {
            this.invincibility = invicibilityIn;
            this.speed = distanceIn / durationIn;
        }

        protected override void PreActions () {
            base.PreActions ();
            User.Animations.SetTrigger ("PlayerRoll");
            Debug.Log ("Rolling");
            if (invincibility) {
                SpriteRenderer[] spriteRenderers = User.Transform.GetComponentsInChildren<SpriteRenderer> ();
                foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                    spriteRenderer.color = Color.red;
                }
            }
        }

        protected override void PostActions () {
            base.PostActions ();
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