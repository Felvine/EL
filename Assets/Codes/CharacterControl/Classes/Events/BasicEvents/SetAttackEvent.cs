
namespace Znko.Events
{
    class SetAttackEvent : CharacterEvent
    {
        public enum Type { All};
        private bool value;
        private Type type;
        public SetAttackEvent (bool valueIn) : base()
        {
            this.type = Type.All;
            this.value = valueIn;
        }
        public override void Do()
        {
            switch (type) {
                case Type.All:
                    this.User.Properties.IsAttacking = value;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException ();
            }
        }

        public override string ToString()
        {
            return "Attack Event Set to " + (value ? "true" : "false");
        }
    }
}
