using Znko.Characters;
namespace Znko.AI
{
    class PolarZone : Zone
    {
        private float radius;
        private Root.Angle startAngle;
        private Root.Angle stopAngle;

        public PolarZone(Character userIn, float radiusIn, Root.Angle startAngleIn, Root.Angle stopAngleIn) : base(userIn)
        {
            this.radius = radiusIn;
            this.startAngle = startAngleIn;
            this.stopAngle = stopAngleIn;
        }
    }
}
