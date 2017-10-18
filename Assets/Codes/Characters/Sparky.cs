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


            float attackDuration = sparky.Animation.GetClip ("Monster_Simple_Attack").length;
            sparky.AddAction ("Bite", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Simple_Attack"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));
            attackDuration = sparky.Animation.GetClip ("Monster_Bodycheck").length;
            sparky.AddAction ("Headbutt", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Bodycheck"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));
            attackDuration = sparky.Animation.GetClip ("Monster_Tail_Swipe").length;
            sparky.AddAction ("TailSwipe", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Tail_Swipe"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));

        }
    }
}
