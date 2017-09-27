using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CharacterAttribute {
    public enum Type { Attack, Defense}

    private int value;

    public int Value {
        get {
            return value;
        }

        set {
            this.value = value;
        }
    }

    public CharacterAttribute (int valueIn) {
        this.value = valueIn;
    }
}
