using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Codes.CharacterControl.Classes.Events
{
    public abstract class CharacterEvent : ICharacterEvent
    {
        private Character user;

        protected Character User {
            get {
                return user;
            }
        }

        public abstract void Do();

        public void SetUser(Character userIn)
        {
            this.user = userIn;
        }
    }
}
