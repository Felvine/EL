namespace Actions {
    class ActionSequence : IAction {
        public Phase Execute () {
            throw new System.NotImplementedException ();
        }
        public virtual bool IsPrimitive () {
            return false;
        }
    }
}
