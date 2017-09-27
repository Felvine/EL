namespace Actions {
    public enum Phase { NotActing, Acting };

    public interface ICharacterAction {
        int Priority { get; }
        Character User { get; }

        Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction);
        void PreActions (ICharacterAction previousAction);
        void PostActions (ICharacterAction nextAction);
        bool IsFinishing ();
        float GetDuration ();
    }

    public static class Constants {
        public const float minimumStep = 0.01f;
    }
}

