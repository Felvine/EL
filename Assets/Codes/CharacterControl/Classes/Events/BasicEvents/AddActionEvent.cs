using Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Codes.CharacterControl.Classes.Events
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
