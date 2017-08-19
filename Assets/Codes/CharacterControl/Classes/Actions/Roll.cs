using System;
using UnityEngine;
namespace Actions {
    class Roll : Move {
        private bool invincibility = false;
        public Roll (Character characterIn, float durationIn, float distanceIn) : base (characterIn, durationIn, distanceIn / durationIn) {

        }

        protected override void PerformAction () {
            base.PerformAction ();
        }

        public override bool IsPrimitive () {
            return false;
        }
    }
}