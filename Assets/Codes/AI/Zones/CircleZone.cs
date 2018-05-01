using Znko.Characters;
using Znko.Root;

namespace Znko.AI {
    public class CircleZone : Zone
    {
        private float radius;

        public CircleZone(Character userIn, float radiusIn) : base(userIn)
        {
            this.radius = radiusIn;
        }

        public override bool IsIn(Coord coord)
        {
            if (Root.Coord.Distance(coord, User.GetCoord()) > radius)
                return false;
            return true;
        }
    }
}
