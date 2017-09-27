using UnityEngine;
using Actions;
namespace Characters {
    class Sparky {
        private const int walkSpeed = 6;

        public static Character Create (Transform transformIn) {
            Character sparky = new Character (transformIn);
            SetupActions (ref sparky);

            return sparky;
        }

        private static void SetupActions (ref Character sparky) {
            sparky.AddAction ("Idle", new Idle (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Idle")));
            sparky.AddAction ("Walk", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk"), walkSpeed));
        }
    }
}
