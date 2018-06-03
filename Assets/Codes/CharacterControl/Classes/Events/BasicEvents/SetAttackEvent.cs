
using System;
using Znko.Actions;
using Znko.Characters;

namespace Znko.Events
{
    static class StaticEvents
    {

        public static void EnableAttack(Character user, ICharacterAction previousOrNextAction, ICharacterAction currentAction, EventArgs e = null)
        {
            user.Properties.IsAttacking = true;
        }

        public static void DisableAttack(Character user, ICharacterAction previousOrNextAction, ICharacterAction currentAction, EventArgs e = null)
        {
            user.Properties.IsAttacking = false;
        }

        public static void EnableInvulnerability(Character user, ICharacterAction previousOrNextAction, ICharacterAction currentAction, EventArgs e = null)
        {
            user.Properties.IsInvulnerable = true;
        }

        public static void DisableInvulnerability(Character user, ICharacterAction previousOrNextAction, ICharacterAction currentAction, EventArgs e = null)
        {
            user.Properties.IsInvulnerable = false;
        }

    }
}
