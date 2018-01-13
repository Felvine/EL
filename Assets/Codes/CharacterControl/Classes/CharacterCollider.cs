using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Codes.CharacterControl.Classes
{
    public class CharacterCollider
    {
        private string tagType;

        private bool isAttacking;
        //private bool isSolid; TODO out of scope

        public bool IsAttacking {
            get {
                return isAttacking;
            }

            set {
                isAttacking = value;
            }
        }

        public CharacterCollider(string typeIn)
        {
            this.tagType = typeIn;
        }

        public bool IsType (string typeIn)
        {
            return tagType == typeIn;
        }
    }
}
