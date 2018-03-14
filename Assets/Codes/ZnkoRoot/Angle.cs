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
    }
}
