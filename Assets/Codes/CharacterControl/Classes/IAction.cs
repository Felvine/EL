namespace Actions {
    public enum Phase { NotActing, Acting };

    public interface IAction {
        Phase Execute ();
        bool IsPrimitive ();
    }
}

