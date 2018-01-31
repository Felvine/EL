using UnityEngine;
using Znko.Actions;
using Znko.Events;
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
            sparky.AddAction ("Idle", new Idle (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Idle"), 0));
            sparky.AddAction ("Walk", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk"), walkSpeed));
            sparky.AddAction ("WalkBackwards", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk_Backward"), walkBackwardSpeed));
            sparky.AddAction ("Run", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Run"), MonsterRunSpeed));
            sparky.AddAction ("Jump", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Jump"), MonsterJumpLength));



            float attackDuration = sparky.Animation.GetClip ("Monster_Simple_Attack").length;
            ActionEvent[] attackEvents = {  new ActionEvent(ActionEvent.Phase.PreAction, new SetAttackEvent(true)),
                                            new ActionEvent(ActionEvent.Phase.PostAction, new SetAttackEvent(false)) };

            sparky.AddAction ("Bite", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Simple_Attack"),
                                                            new Idle (sparky, attackDuration*70 / 160, null, 1),
                                                            new Idle (sparky, attackDuration*15 / 160/100, null, 1, attackEvents),
                                                            new Idle (sparky, attackDuration*75 / 160/100, null, 1)));
            attackDuration = sparky.Animation.GetClip ("Monster_Bodycheck").length;
            sparky.AddAction ("Headbutt", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Bodycheck"),
                                                            new Idle(sparky, attackDuration*35 / 118, null, 1),
                                                            new Idle(sparky, attackDuration*8 / 118, null, 1, attackEvents),
                                                            new Idle(sparky, attackDuration*75 / 118, null, 1)));

            attackDuration = sparky.Animation.GetClip ("Monster_Tail_Swipe").length;
            sparky.AddAction ("TailSwipe", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Tail_Swipe"),
                                                            new Idle(sparky, attackDuration*50 / 195, null, 1),
                                                            new Idle(sparky, attackDuration*8 / 195, null, 1, attackEvents),
                                                            new Idle(sparky, attackDuration*137 / 195, null, 1)));

            attackDuration = sparky.Animation.GetClip ("Monster_Rush_Bodycheck").length;
            sparky.AddAction ("RushHeadbutt", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Rush_Bodycheck"),
                                                            new Idle(sparky, attackDuration*70 / 253, null, 1),
                                                            new Idle(sparky, attackDuration*100 / 235, null, 1, attackEvents),
                                                            new Idle(sparky, attackDuration*83 / 253, null, 1, attackEvents)));

            attackDuration = sparky.Animation.GetClip ("Monster_Jump_Attack").length;
            sparky.AddAction ("JumpAttack", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Jump_Attack"),
                                                            new Idle(sparky, attackDuration / 3, null, 1),
                                                            new Idle(sparky, attackDuration / 3, null, 1, attackEvents),
                                                            new Idle(sparky, attackDuration / 3, null, 1)));
        }
    }
}
