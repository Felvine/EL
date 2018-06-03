using UnityEngine;
using Znko.Actions;
using Znko.Events;
using Znko.AI;
using Znko.Characters;
using Znko.Root;

namespace Characters {
    class Sparky {
        private const float walkSpeed = 5.9f;
        private const int walkBackwardSpeed = 5;
        private const int monsterRunSpeed = 10;
        private const int monsterJumpLength = 10;

        private const float underRadius = 2f;
        private const float closeRadius = 6f;
        private const float moderateRadius = 20f;
        private const float farRadius = 50f;

        private static Coord frontOffSet = new Coord(-3f, 0.75f);
        private const float fleeDistance = 8f;


        public static Character Create (Transform transformIn) {
            Character sparky = new Character (transformIn);
            sparky.Faction = Character.Factions.Enemy;
            sparky.Race = Character.Races.Giant;
            SetupActions (ref sparky);
            SetupZones(ref sparky);
            return sparky;
        }

        private static void SetupActions (ref Character sparky) {
            sparky.AddAction ("Idle", new Idle (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Idle"), 0));
            sparky.AddAction ("Walk", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk"), walkSpeed, 1));
            sparky.AddAction ("WalkBackwards", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Walk_Backward"), walkBackwardSpeed, 1));
            sparky.AddAction ("Run", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Run"), monsterRunSpeed, 1));
            //sparky.AddAction ("Jump", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Jump"), monsterJumpLength));



            float attackDuration = sparky.Animation.GetClip ("Monster_Simple_Attack").length;
            ActionEvent[] attackEvents = {  new ActionEvent(ActionEvent.Phase.PreAction, new SetAttackEvent(true)),
                                            new ActionEvent(ActionEvent.Phase.PostAction, new SetAttackEvent(false)) };

            sparky.AddAction ("Bite", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Simple_Attack"), null,
                                                            new Idle (sparky, attackDuration*70 / 160, null, 1),
                                                            new Idle (sparky, attackDuration*15 / 160, null, 1, null, attackEvents),
                                                            new Idle (sparky, attackDuration*75 / 160, null, 1)));
            attackDuration = sparky.Animation.GetClip("Monster_Bodycheck").length;
            sparky.AddAction("Headbutt", new CharacterActionSequence(sparky, sparky.Animation.GetClip("Monster_Bodycheck"), null,
                                                            new Idle(sparky, attackDuration * 35 / 118, null, 1),
                                                            new Idle(sparky, attackDuration * 8 / 118, null, 1, null, attackEvents),
                                                            new Idle(sparky, attackDuration * 75 / 118, null, 1)));

            attackDuration = sparky.Animation.GetClip("Monster_Tail_Swipe").length;
            sparky.AddAction("TailSwipe", new CharacterActionSequence(sparky, sparky.Animation.GetClip("Monster_Tail_Swipe"), null,
                                                            new Idle(sparky, attackDuration * 50 / 195, null, 1),
                                                            new Idle(sparky, attackDuration * 8 / 195, null, 1, null, attackEvents),
                                                            new Idle(sparky, attackDuration * 137 / 195, null, 1)));

            attackDuration = sparky.Animation.GetClip("Monster_Rush_Bodycheck").length;
            sparky.AddAction("RushHeadbutt", new CharacterActionSequence(sparky, sparky.Animation.GetClip("Monster_Rush_Bodycheck"), null,
                                                            new Idle(sparky, attackDuration * 70 / 253, null, 1),
                                                            new Idle(sparky, attackDuration * 100 / 235, null, 1, null, attackEvents),
                                                            new Idle(sparky, attackDuration * 83 / 253, null, 1, null, attackEvents)));

            attackDuration = sparky.Animation.GetClip("Monster_Jump_Attack").length;
            sparky.AddAction("JumpAttack", new CharacterActionSequence(sparky, sparky.Animation.GetClip("Monster_Jump_Attack"), null,
                                                            new Idle(sparky, attackDuration * 56 / 116, null, 1),
                                                            new MoveToTarget(sparky, attackDuration * 29 / 116, null, frontOffSet),
                                                            new Idle(sparky, attackDuration * 10 / 116, null, 1, null, attackEvents),
                                                            new Idle(sparky, attackDuration * 21 / 116, null, 1)));

            attackDuration = sparky.Animation.GetClip("Monster_Jump_Attack").length;
            sparky.AddAction("Jump", new CharacterActionSequence(sparky, sparky.Animation.GetClip("Monster_Jump_Attack"), null,
                                                            new Idle(sparky, attackDuration * 56 / 116, null, 1),
                                                            new FleeToDistance(sparky, attackDuration * 29 / 116, null, fleeDistance),
                                                            new Idle(sparky, attackDuration * 31 / 116, null, 1)));

            sparky.AddAction("LongSilence", new Idle(sparky, 20, sparky.Animation.GetClip("Monster_Idle"), 1));
        }

        private static void SetupZones (ref Character sparky)
        {
            CircleZone underZone = new CircleZone(sparky, underRadius);
            CircleZone closeZone = new CircleZone(sparky, closeRadius);
            CircleZone moderateZone = new CircleZone(sparky, moderateRadius);
            CircleZone farZone = new CircleZone(sparky, farRadius);

            PolarZone frontZone = new PolarZone(sparky, closeRadius, 45, 135, false);
            PolarZone bottomZone = new PolarZone(sparky, closeRadius, 135, -135, true);
            PolarZone backZone = new PolarZone(sparky, closeRadius, -135, -45, false);
            PolarZone topZone = new PolarZone(sparky, closeRadius, -45, 45, true);

            sparky.AddZones(underZone, "underZone");
            sparky.AddZones(moderateZone, "moderateZone");
            sparky.AddZones(closeZone, "closeZone");
            sparky.AddZones(farZone, "farZone");

            sparky.AddZones(frontZone, "frontZone");
            sparky.AddZones(bottomZone, "bottomZone");
            sparky.AddZones(backZone, "backZone");
            sparky.AddZones(topZone, "topZone");
        }
    }
}
