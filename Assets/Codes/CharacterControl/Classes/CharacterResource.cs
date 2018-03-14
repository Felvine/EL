using UnityEngine;
namespace Znko.Characters
{
    public class CharacterResource
    {
        public enum Type { Health, Mana, Stamina };

        private int value;
        private int maxValue;
        private UnityEngine.Color color;
        private float regenRate;

        public CharacterResource()
        {
            this.value = 0;
            this.maxValue = 0;
            this.color = Color.grey;
            this.regenRate = 0.0f;
        }

        public CharacterResource(int maxValueIn, Color colorIn)
        {
            this.maxValue = maxValueIn;
            this.value = maxValueIn;
            this.color = colorIn;
            this.regenRate = 0.0f;
        }

        internal void Decrease(int v)
        {
            this.value = this.value - v;
        }

        public Color Color {
            get {
                return color;
            }

            set {
                color = value;
            }
        }

        public float Percentage {
            get {
                return (float)value / (float)maxValue;
            }
        }
    }
}