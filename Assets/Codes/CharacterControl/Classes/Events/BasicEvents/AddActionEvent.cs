using Znko.Actions;

namespace Znko.Events
{
    class AddActionEvent : CharacterEvent
    {
        private ICharacterAction action;

        public AddActionEvent(ICharacterAction actionIn)
        {
            this.action = actionIn;
        }

        public override void Do()
        {
        }
        public ICharacterAction GetAction()
        {
            return action;
        }
    }
}
