using UnityEngine;

namespace Assets.Codes.CharacterControl.Classes.Events
{
    class ReceiveDamageEvent : CharacterEvent
    {
        private int dmg;
        public ReceiveDamageEvent(int dmgIn)
        {
            this.dmg = dmgIn;
        }

        public override void Do()
        {
            this.User.GetResource(CharacterResource.Type.Health).Decrease(dmg);
        }
    }
}
