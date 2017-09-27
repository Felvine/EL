using UnityEngine;
using Actions;

namespace Characters {
    static class Player {
        private const float playerWalkSpeed = 6.0f;
        private const float playerRunSpeed = 10.0f;
        private const float playerRollLength = 7.5f;

        private static Character player;

        public static Character Instance () {
            if (player == null)
                Create (GameObject.FindGameObjectWithTag("Player").transform);
            return player;
        }

        public static void Create (Transform transformIn) {
            player = new Character (transformIn);
            SetupActions (ref player);
        }

        private static void SetupActions (ref Character player) {
            player.AddAction ("Walk", new MoveWithSpeed (player, Constants.minimumStep, player.Animation.GetClip ("Player_Walk"), playerWalkSpeed));

            player.AddAction ("Run", new MoveWithSpeed (player, Constants.minimumStep, player.Animation.GetClip ("Player_Run"), playerRunSpeed));

            player.AddAction ("Roll", new MoveToDistance (player, player.Animation.GetClip ("Player_Roll").length, player.Animation.GetClip ("Player_Roll"), playerRollLength, false));

            player.AddAction ("Idle", new Idle (player, Constants.minimumStep, player.Animation.GetClip ("Player_Idle")));

            ComboAction attack1ending = new ComboAction (new Idle (player, player.Animation.GetClip ("Player_Attack_Ender").length, player.Animation.GetClip ("Player_Attack_Ender")),
                                                         new Attack (player, player.Animation.GetClip ("Player_Attack_3").length, player.Animation.GetClip ("Player_Attack_3")),
                                                         new Attack (player, player.Animation.GetClip ("Player_Attack_2").length, player.Animation.GetClip ("Player_Attack_2")));

            player.AddAction ("ComboAttack", new CharacterActionSequence (player, null,
                                                            new Attack (player, player.Animation.GetClip ("Player_Attack_1").length, player.Animation.GetClip ("Player_Attack_1")),
                                                            attack1ending));

            float attack4duration = player.Animation.GetClip ("Player_Attack_4").length;
            player.AddAction ("Attack4", new CharacterActionSequence (player, player.Animation.GetClip ("Player_Attack_4"),
                                                            new Idle (player, attack4duration / 3, null),
                                                            new Attack (player, attack4duration / 3, null),
                                                            new Idle (player, attack4duration / 3, null)));
        }
    }
}