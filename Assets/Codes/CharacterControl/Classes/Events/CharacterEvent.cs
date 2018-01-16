
namespace Znko.Events
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
