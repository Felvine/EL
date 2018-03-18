using UnityEngine;

namespace Znko.Root
{
    class Angle
    {
        private int value;
        private bool overflow;

        public Angle(int valueIn)
        {
            if (valueIn >= 360)
                this.overflow = true;
            else
                this.overflow = false;

            this.value = valueIn % 360;
        }

        public Angle (Coord c1, Coord c2)
        {
            float dX = c1.X - c2.X;
            float dY = c1.Y - c2.Y;
            float rad = Mathf.Atan2(dY, dX)-Mathf.PI/2;
            int deg = Mathf.RoundToInt(rad / Mathf.PI * 180) % 360;
            if(deg > 180)
            {
                deg = deg - 360;
            }
            if (deg < -180)
            {
                deg = deg + 360;
            }
            this.value = deg;
        }

        public int Value {
            get {
                return value;
            }
        }

        public static implicit operator Angle(int a)
        {
            Angle angle = new Angle(a);
            return angle;
        }

        public static bool operator <(Angle left, Angle right)
        {
            return left.value < right.value;
        }

        public static bool operator >(Angle left, Angle right)
        {
            return left.value > right.value;
        }


        public static Angle operator *(Angle left, int right)        {
            return new Angle(left.value * right);
        }

        public static Angle operator +(Angle left, int right)
        {
            return new Angle(left.value + right);
        }

        public override string ToString() 
        {
            return this.value.ToString ();
        }
    }
}
