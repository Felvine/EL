using UnityEngine;
namespace Znko.Characters
{
    public class CharacterResource
    {
        public enum Type { Health, Mana, Stamina };

        private float _value;
        private int _maxValue;
        private UnityEngine.Color _color;
        private float _regenRate;

        public CharacterResource()
        {
            this._value = 0;
            this._maxValue = 0;
            this._color = Color.grey;
            this._regenRate = 0.0f;
        }

        public CharacterResource(int maxValueIn, Color colorIn, float regenRate = 0.0f)
        {
            this._maxValue = maxValueIn;
            this._value = maxValueIn;
            this._color = colorIn;
            this._regenRate = regenRate;
        }

        internal void Decrease(int v)
        {
            this._value = this._value - v;
        }

        public Color Color {
            get {
                return _color;
            }

            set {
                _color = value;
            }
        }

        public float Percentage {
            get {
                return (float)_value / (float)_maxValue;
            }
        }

        public float Value {
            get {
                return _value;
            }
            set {
                this._value = value;
            }
        }

        public float RegenRate { get { return _regenRate; } }

        public float MaxValue { get { return _maxValue; } }
    }
}