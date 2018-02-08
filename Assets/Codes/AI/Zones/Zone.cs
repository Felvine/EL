using Znko.Characters;

namespace Znko.AI
{
    public class Zone
    {
        private Character user;
        private Root.Coord offset;

        public Zone(Character userIn)
        {
            this.user = userIn;
            this.offset = new Root.Coord();
        }
    }
}
