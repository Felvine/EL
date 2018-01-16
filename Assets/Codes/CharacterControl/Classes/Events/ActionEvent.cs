
namespace Znko.Events
{
   public  class ActionEvent
    {
        public enum Phase { PreAction, PostAction }
        private Phase phase;
        private ICharacterEvent characterEvent;
        public ActionEvent(ActionEvent.Phase phaseIn, ICharacterEvent eventIn)
        {
            this.phase = phaseIn;
            this.characterEvent = eventIn;
        }
        public Phase GetPhase() { return this.phase; }
        public ICharacterEvent Value{
            get {
                return this.characterEvent;
            }
        }
    }
}
