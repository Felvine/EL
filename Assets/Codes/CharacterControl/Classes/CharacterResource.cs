using System;
using UnityEngine;

class CharacterResource {
    private int value;
    private int maxValue;
    private UnityEngine.Color color;
    private float regenRate;

    public CharacterResource (int maxValueIn, UnityEngine.Color colorIn) {
        this.maxValue = maxValueIn;
        this.value = maxValueIn;
        this.color = colorIn;
        this.regenRate = 0.0f;
    }

    internal void Decrease (int v) {
        this.value = this.value - v;
        Debug.Log (value);
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
            Debug.Log (value);
            Debug.Log (maxValue);
            return (float)value / (float)maxValue;

        }
    }
}
