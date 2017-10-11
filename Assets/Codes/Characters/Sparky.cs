using UnityEngine;
using Actions;
namespace Characters {
    class Sparky {
        private const int walkSpeed = 6;

        public static Character Create (Transform transformIn) {
            Character sparky = new Character (transformIn);
            sparky.Faction = Character.Factions.Enemy;
            SetupActions (ref sparky);

            return sparky;
        }

        private static void SetupActions (ref Character sparky) {
            sparky.AddAction ("Idle", new Idle (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Idle")));
            sparky.AddAction ("Walk", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk"), walkSpeed));


            float attack4duration = sparky.Animation.GetClip ("Monster_Simple_Attack").length;
            sparky.AddAction ("Bite", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Simple_Attack"),
                                                            new Idle (sparky, attack4duration / 3, null),
                                                            new Attack (sparky, attack4duration / 3, null),
                                                            new Idle (sparky, attack4duration / 3, null)));

            sparky.AddAction ("Headbutt", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Bodycheck"),
                                                            new Idle (sparky, attack4duration / 3, null),
                                                            new Attack (sparky, attack4duration / 3, null),
                                                            new Idle (sparky, attack4duration / 3, null)));

            sparky.AddAction ("TailSwipe", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Tail_Swipe"),
                                                            new Idle (sparky, attack4duration / 3, null),
                                                            new Attack (sparky, attack4duration / 3, null),
                                                            new Idle (sparky, attack4duration / 3, null)));

        }
    }
}
