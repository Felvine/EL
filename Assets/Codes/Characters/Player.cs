using UnityEngine;
using Znko.Actions;
using Znko.Events;
using Znko.Characters;

namespace Characters {
    static class Player {
        private const float playerWalkSpeed = 4.30f;
        private const float playerRunSpeed = 7.00f;
        private const float playerRollLength = 5.05f;

        private static Character player;

        public static Character Instance () {
            if (player == null)
                Create (GameObject.FindGameObjectWithTag("Player").transform);
            return player;
        }

        public static void Create (Transform transformIn) {
            player = new Character (transformIn);
            player.Faction = Character.Factions.Player;
            player.Properties.SetResource(CharacterResource.Type.Health, new CharacterResource(100, Color.green));
            player.Properties.SetResource(CharacterResource.Type.Stamina, new CharacterResource(100, Color.yellow, 0.1f));
            SetupActions (ref player);
        }

        private static void SetupActions (ref Character player) {
            ActionEvent[] attackEvents = {  new ActionEvent(ActionEvent.Phase.PreAction, new SetAttackEvent(true)),
                                            new ActionEvent(ActionEvent.Phase.PostAction, new SetAttackEvent(false)) };

            ActionEvent[] invulnerabilityEvents = {  new ActionEvent(ActionEvent.Phase.PreAction, new SetInvulnerabilityEvent(true)),
                                                     new ActionEvent(ActionEvent.Phase.PostAction, new SetInvulnerabilityEvent(false)) };


            player.AddAction ("Walk", new MoveWithSpeed (player, Constants.minimumStep, player.Animation.GetClip ("Player_Walk"), playerWalkSpeed, 0));

            player.AddAction ("Run", new MoveWithSpeed (player, Constants.minimumStep, player.Animation.GetClip ("Player_Run"), playerRunSpeed, 0));

            player.AddAction ("Roll", new MoveToDistance (player, player.Animation.GetClip ("Player_Roll").length, player.Animation.GetClip ("Player_Roll"), playerRollLength, null, invulnerabilityEvents));

            player.AddAction ("Idle", new Idle (player, Constants.minimumStep, player.Animation.GetClip ("Player_Idle"), 0));

            float attack1duration = player.Animation.GetClip("Player_Attack_1").length; 
            float attack2duration = player.Animation.GetClip("Player_Attack_2").length;
            float attack3duration = player.Animation.GetClip("Player_Attack_3").length;  

            ComboAction attack1ending = new ComboAction (new Idle (player, player.Animation.GetClip ("Player_Attack_Ender").length, player.Animation.GetClip ("Player_Attack_Ender"), 1),
                                                         new CharacterActionSequence(player, player.Animation.GetClip("Player_Attack_3"), null,
                                                            new Idle(player, attack3duration*48 / 139, null, 1),
                                                            new Idle(player, attack3duration*10 / 139, null, 1, null, attackEvents),
                                                            new Idle(player, attack3duration*81 / 139, null, 1)),
                                                         new CharacterActionSequence(player, player.Animation.GetClip("Player_Attack_2"), null,
                                                            new Idle(player, attack2duration*16 / 161, null, 1),
                                                            new Idle(player, attack2duration*20 / 161, null, 1, null, attackEvents),
                                                            new Idle(player, attack2duration*125 / 161, null, 1)));

            player.AddAction ("ComboAttack", new CharacterActionSequence (player, player.Animation.GetClip("Player_Attack_1"), null,
                                                            new Idle(player, attack1duration*24 / 40, null, 1),
                                                            new Idle(player, attack1duration*6 / 40, null, 1, null, attackEvents),
                                                            new Idle(player, attack1duration*10 / 40, null, 1),
                                                            attack1ending));

            float attack4duration = player.Animation.GetClip("Player_Attack_4").length;
            player.AddAction("Attack4", new CharacterActionSequence(player, player.Animation.GetClip("Player_Attack_4"), new ResourceCost(CharacterResource.Type.Stamina, 38),
                                                            new Idle(player, attack4duration*24 / 57, null, 1),
                                                            new Idle(player, attack4duration*10 / 57, null, 1, null, attackEvents),
                                                            new Idle(player, attack4duration*23 / 57, null, 1)));

            float staggerduration = player.Animation.GetClip("Player_Stagger").length;
            player.AddAction("Stagger", new Idle(player, staggerduration, player.Animation.GetClip("Player_Stagger"), 1));

            staggerduration = player.Animation.GetClip("Player_Fall").length;
            player.AddAction("Fall", new Idle(player, staggerduration, player.Animation.GetClip("Player_Fall"), 3));
        }
    }
}