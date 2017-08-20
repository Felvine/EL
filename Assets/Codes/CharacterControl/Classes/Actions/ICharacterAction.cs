namespace Actions {
    public enum Phase { NotActing, Acting };

    public interface ICharacterAction {
        Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction);
        void PreActions (ICharacterAction previousAction);
        void PostActions (ICharacterAction nextAction);
    }
}

