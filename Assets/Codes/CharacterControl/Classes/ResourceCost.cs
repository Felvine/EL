using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Znko.Characters
{
    public class ResourceCost
    {
        private CharacterResource.Type _type;
        private float _amount;
        private static ResourceCost _zero = new ResourceCost(CharacterResource.Type.Mana, 0);

        public CharacterResource.Type Type {
            get {
                return _type;
            }

            set {
                _type = value;
            }
        }

        public float Amount {
            get {
                return _amount;
            }

            set {
                _amount = value;
            }
        }

        public static ResourceCost Zero {
            get {
                return _zero;
            }
        }

        public ResourceCost(CharacterResource.Type type, float amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
