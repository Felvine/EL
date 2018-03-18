using Znko.Characters;
using Znko.Root;

namespace Znko.AI
{
    class PolarZone : Zone
    {
        private float radius;
        private Root.Angle startAngle;
        private Root.Angle stopAngle;
        private bool overflow;

        public PolarZone(Character userIn, float radiusIn, Root.Angle startAngleIn, Root.Angle stopAngleIn, bool overflowIn) : base(userIn)
        {
            this.radius = radiusIn;
            this.startAngle = startAngleIn;
            this.stopAngle = stopAngleIn;
            this.overflow = overflowIn;
        }

        public override bool IsIn(Coord coord)
        {
            if (Root.Coord.Distance(coord, User.GetCoord()) > radius)
                return false;

            Root.Angle angle= new Angle(coord, User.GetCoord());
            if (User.GetHorizontalDirection() == Character.HorizontalDirection.East)
                angle = angle * -1;

            if (!overflow)
            {
                if (angle < startAngle || angle > stopAngle)
                    return false;
            }
            else
            {
                if (startAngle> 0)
                {
                    if (angle > 0 && angle < startAngle)
                        return false;
                    if (angle < 0 && angle > stopAngle)
                        return false;
                }
                else
                {
                    if (angle > 0 && angle > stopAngle)
                        return false;
                    if (angle < 0 && angle < startAngle)
                        return false;
                }
            }
            return true;
        }
    }
}
