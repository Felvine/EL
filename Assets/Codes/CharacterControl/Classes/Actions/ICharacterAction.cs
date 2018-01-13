﻿namespace Actions {
    public enum Phase { NotActing, Acting };

    public interface ICharacterAction {
        int Priority { get; }
        Character User { get; }

        Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction, ICharacterController controller);
        void PreActions (ICharacterAction previousAction, ICharacterController controller);
        void PostActions (ICharacterAction nextAction, ICharacterController controller);
        bool IsFinishing ();
        bool CanInterrupt(ICharacterAction currentAction);
        float GetDuration ();
    }

    public static class Constants {
        public const float minimumStep = 0.01f;
    }
}

