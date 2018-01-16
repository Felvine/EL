using System;
using System.Collections.Generic;


namespace Znko.Actions {
    class ComboAction : ICharacterAction {
        private List<ICharacterAction> actions;
        private int selected = 0;

        public int Priority {
            get {
                return actions[Selected].Priority;
            }
        }

        public Character User {
            get {
                return actions[Selected].User;
            }
        }

        public int Selected {
            get {
                if (selected < actions.Count && selected >= 0)
                    return selected;
                else
                    return 0;
            }

            set {
                if (value < actions.Count && value >= 0)
                    selected = value;
                else
                    throw new ArgumentOutOfRangeException ();
            }
        }
        public ComboAction (params ICharacterAction[] actionsIn) {
            this.actions = new List<ICharacterAction> ();
            for (int i = 0; i < actionsIn.Length; i++) {
                this.actions.Add (actionsIn[i]);
            }
            Selected = 0;
        }

        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction, ICharacterController controller) {
            Phase result = actions[Selected].Execute (previousAction, nextAction, controller);
            if (result == Phase.NotActing)
                Selected = 0;
            return result;
        }

        public float GetDuration () {
            return actions[Selected].GetDuration ();
        }

        public bool IsFinishing () {
            return actions[Selected].IsFinishing ();
        }

        public void PostActions (ICharacterAction nextAction, ICharacterController controller) {
            //actions[Selected].PostActions (nextAction);
            Selected = 0;
        }

        public void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            //actions[Selected].PreActions (previousAction);
        }

        public bool CanInterrupt(ICharacterAction currentAction)
        {
            return false;
        }
    }
}