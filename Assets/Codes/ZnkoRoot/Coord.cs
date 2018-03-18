using System;
using UnityEngine;

namespace Znko.Root
{
    public class Coord
    {
        private float x;
        private float y;

        public float X {
            get {
                return x;
            }
        }

        public float Y {
            get {
                return y;
            }
        }

        public Coord()
        {
            this.x = 0;
            this.y = 0;
        }
        public Coord(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Coord (Vector3 vector)
        {
            return new Coord(vector.x, vector.z);
        }

        public static float Distance (Coord p1, Coord p2)
        {
            return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
        }


    }
}
