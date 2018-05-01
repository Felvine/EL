using Znko.Characters;

namespace Znko.AI
{
    public abstract class Zone
    {
        private Character user;
        private Root.Coord offset;

        public Zone(Character userIn)
        {
            this.user = userIn;
            this.offset = new Root.Coord();
        }

        protected Character User {
            get {
                return user;
            }

            set {
                user = value;
            }
        }

        public abstract bool IsIn(Root.Coord coord);
    }
}
