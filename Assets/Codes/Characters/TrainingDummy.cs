using Znko.Actions;
using Znko.Characters;
using UnityEngine;

namespace Characters {
    static class TrainingDummy {
        public static Character Create (Transform transformIn) {
            Character dummy = new Character (transformIn);
            SetupActions (ref dummy);

            return dummy;
        }

        private static void SetupActions (ref Character dummy) {
            dummy.AddAction ("ReceiveHit", new Idle (dummy, dummy.Animation.GetClip ("Dummy_Hit").length, dummy.Animation.GetClip ("Dummy_Hit"), 1));
        }
    }
}
