using UnityEngine;
using Actions;
namespace Characters {
    class Sparky {
        private const float walkSpeed = 5.9f;
        private const int walkBackwardSpeed = 5;
        private const int MonsterRunSpeed = 10;
        private const int MonsterJumpLength = 10;

        public static Character Create (Transform transformIn) {
            Character sparky = new Character (transformIn);
            sparky.Faction = Character.Factions.Enemy;
            sparky.Race = Character.Races.Giant;
            SetupActions (ref sparky);

            return sparky;
        }

        private static void SetupActions (ref Character sparky) {
            sparky.AddAction ("Idle", new Idle (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Idle")));
            sparky.AddAction ("Walk", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk"), walkSpeed));
            sparky.AddAction ("WalkBackwards", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk_Backward"), walkBackwardSpeed));
            sparky.AddAction ("Run", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Run"), MonsterRunSpeed));
            sparky.AddAction ("Jump", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Jump"), MonsterJumpLength));



            float attackDuration = sparky.Animation.GetClip ("Monster_Simple_Attack").length;
            sparky.AddAction ("Bite", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Simple_Attack"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));
            attackDuration = sparky.Animation.GetClip ("Monster_Bodycheck").length;
            /*
            sparky.AddAction ("Headbutt", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Bodycheck"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));*/

            sparky.AddAction ("Headbutt", new Attack (sparky, attackDuration, null));
            attackDuration = sparky.Animation.GetClip ("Monster_Tail_Swipe").length;
            sparky.AddAction ("TailSwipe", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Tail_Swipe"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));
            attackDuration = sparky.Animation.GetClip ("Monster_Rush_Bodycheck").length;
            sparky.AddAction ("RushHeadbutt", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Rush_Bodycheck"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null)));
            attackDuration = sparky.Animation.GetClip ("Monster_Jump_Attack").length;
            sparky.AddAction ("JumpAttack", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Jump_Attack"),
                                                            new Idle (sparky, attackDuration / 3, null),
                                                            new Attack (sparky, attackDuration / 3, null),
                                                            new Idle (sparky, attackDuration / 3, null)));
        }
    }
}
