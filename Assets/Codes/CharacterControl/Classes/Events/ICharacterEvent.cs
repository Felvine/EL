using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Codes.CharacterControl.Classes.Events
{
    public interface ICharacterEvent
    {
        void Do();
        void SetUser(Character userIn);
    }
}
