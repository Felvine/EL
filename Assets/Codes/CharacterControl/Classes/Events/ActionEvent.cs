
using Znko.Actions;

namespace Znko.Events
{
   public  class ActionEvent
    {
        public enum ActionPhase { PreAction, PostAction }
        private ActionPhase _phase;
        private ActionEventHandler _eventHandler;
        public ActionEvent(ActionEvent.ActionPhase phaseIn, ActionEventHandler eventIn)
        {
            this._phase = phaseIn;
            this._eventHandler = eventIn;
        }
        public ActionEventHandler Value {
            get {
                return this._eventHandler;
            }
        }

        public ActionPhase Phase {
            get {
                return _phase;
            }
        }
    }
}
