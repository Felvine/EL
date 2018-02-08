﻿using UnityEngine;
using Znko.Actions;
using Znko.Events;
using Znko.AI;
using Znko.Characters;

namespace Characters {
    class Sparky {
        private const float walkSpeed = 5.9f;
        private const int walkBackwardSpeed = 5;
        private const int monsterRunSpeed = 10;
        private const int monsterJumpLength = 10;

        private const float tooCloseRadius = 1.5f;
        private const float closeRadius = 5;

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
            sparky.AddAction ("Run", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Run"), monsterRunSpeed));
            sparky.AddAction ("Jump", new MoveWithSpeed (sparky, Constants.minimumStep, sparky.Animation.GetClip ("Monster_Jump"), monsterJumpLength));



            float attackDuration = sparky.Animation.GetClip ("Monster_Simple_Attack").length;
            ActionEvent[] attackEvents = {  new ActionEvent(ActionEvent.Phase.PreAction, new SetAttackEvent(true)),
                                            new ActionEvent(ActionEvent.Phase.PostAction, new SetAttackEvent(false)) };

            sparky.AddAction ("Bite", new CharacterActionSequence (sparky, sparky.Animation.GetClip ("Monster_Simple_Attack"),
                                                            new Idle (sparky, attackDuration*70 / 160, null, 1),
                                                            new Idle (sparky, attackDuration*15 / 160, null, 1, attackEvents),
                                                            new Idle (sparky, attackDuration*75 / 160, null, 1)));
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
                                                            new Idle(sparky, attackDuration*85 / 116, null, 1),
                                                            new Idle(sparky, attackDuration*10 / 116, null, 1, attackEvents),
                                                            new Idle(sparky, attackDuration*21 / 116, null, 1)));
        }

        private static void SetupZones (ref Character sparky)
        {
            PolarZone underZone = new PolarZone(sparky, tooCloseRadius, 0, 360);
            PolarZone frontZone = new PolarZone(sparky, closeRadius, 45, 135);
            PolarZone bottomZone = new PolarZone(sparky, closeRadius, 135, 225);
            PolarZone backZone = new PolarZone(sparky, closeRadius, 225, 315);
            PolarZone topZone = new PolarZone(sparky, closeRadius, 315, 45);
        }
    }
}
