using System;
using Znko.Characters;

namespace Znko.Actions {
    public enum Phase { NotActing, Acting };

    public delegate void ActionEventHandler (Character user, ICharacterAction previousOrNextAction, ICharacterAction currentAction, EventArgs e = null);

    public interface ICharacterAction {
        event ActionEventHandler PreActionEvent;
        event ActionEventHandler PostActionEvent;
        int Priority { get; }
        Character User { get; }
        ResourceCost Cost { get; }

        Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction, ICharacterController controller);
        void PreActions (ICharacterAction previousAction, ICharacterController controller);
        void PostActions (ICharacterAction nextAction, ICharacterController controller);
        bool IsFinishing ();
        float GetDuration ();
    }

    public static class Constants {
        public const float minimumStep = 0.01f;
    }
}

