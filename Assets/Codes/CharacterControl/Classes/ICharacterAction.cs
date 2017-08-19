namespace Actions {
    public enum Phase { NotActing, Acting };

    public interface ICharacterAction {
        Phase Execute ();
        bool IsPrimitive ();
        bool AlmostDone ();
    }
}

