using System.Collections.Generic;

namespace Actions {
    class ActionSequence : IAction {
        private List<IAction> actions;
        private int step = 0;

        public ActionSequence(params IAction[] actionsIn) {
            this.actions = new List<IAction> ();
            step = 0;
            for (int i = 0; i< actionsIn.Length; i++) {
                this.actions.Add (actionsIn[i]);
            }
        }
        public Phase Execute () {
            if (step == (actions.Count-1)) {
                if (actions[step].Execute () == Phase.NotActing) {
                    step = 0;
                    return Phase.NotActing;
                } else {
                    return Phase.Acting;
                }
            } else {
                if (actions[step].Execute () == Phase.NotActing)
                    step++;
                return Phase.Acting;
            }
        }
        public virtual bool IsPrimitive () {
            return false;
        }
    }
}
