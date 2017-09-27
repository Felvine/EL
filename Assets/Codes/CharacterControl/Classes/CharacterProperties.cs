using System.Collections.Generic;
using UnityEngine;

public  class CharacterProperties {
    private bool isAttacking = false;
    private Dictionary<CharacterResource.Type, CharacterResource> resources;
    private Dictionary<CharacterAttribute.Type, CharacterAttribute> attributes;
    //private float walkSpeed;
    //private float runSpeed;
    //private bool invincible = false;

    public CharacterProperties () {
        this.resources = new Dictionary<CharacterResource.Type, CharacterResource> ();
        this.resources.Add (CharacterResource.Type.Health, new CharacterResource (100, Color.red));

        this.attributes = new Dictionary<CharacterAttribute.Type, CharacterAttribute> ();
        this.attributes.Add (CharacterAttribute.Type.Attack, new CharacterAttribute (10));
}

    public bool IsAttacking {
        get {
            return isAttacking;
        }

        set {
            isAttacking = value;
        }
    }

    public CharacterResource GetResource (CharacterResource.Type type) {
        return resources[type];
    }
}
