using System;
using System.Collections.Generic;
using UnityEngine;
namespace Actions {
    class Roll : Move {
        private bool invincibility;
        public Roll (Character characterIn, float durationIn, float distanceIn, bool invicibilityIn) : base (characterIn, durationIn, distanceIn / durationIn) {
            this.invincibility = invicibilityIn;
        }
        protected override void PreActions () {
            Debug.Log ("lol");
            base.PreActions ();
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
            base.PerformAction ();
        }

        public override bool IsPrimitive () {
            return false;
        }
    }
}