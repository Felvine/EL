using System;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    enum HorizontalDirection { West, East}

    private CharacterProperties properties;
    private Dictionary<string, Actions.ICharacterAction> availableActions = new Dictionary<string, Actions.ICharacterAction> ();

    private Vector3 direction = Vector3.zero;
    private HorizontalDirection horizontalDir = HorizontalDirection.East;

    private Transform transform;
    private Animation animation;
    private CharacterController controller;

    public Animation Animation {
        get {
            return animation;
        }
    }


    public Vector3 Direction {
        get {
            return direction;
        }
        set {
            direction = value;
            SetHorizontalDirection ();
        }
    }

    private void SetHorizontalDirection (){
        if (horizontalDir == HorizontalDirection.East && direction.x < 0) {
            HorizontalDir = HorizontalDirection.West;
        } else if (horizontalDir == HorizontalDirection.West && direction.x > 0) {
            HorizontalDir = HorizontalDirection.East;
        }

    }

    public Transform Transform {
        get {
            return transform;
        }
    }

    private HorizontalDirection HorizontalDir {
        set {
            horizontalDir = value;
            foreach (Transform child in transform) {
                if (child.tag == "Character") {
                    switch (horizontalDir) {
                        case HorizontalDirection.East:
                            child.rotation = Quaternion.Euler (45, 0, 0);
                            child.localScale = new Vector3 (1, 1, 1);
                            break;
                        case HorizontalDirection.West:
                            child.rotation = Quaternion.Euler (-45, 180, 0);
                            child.localScale = new Vector3 (1, 1, -1);
                            break;
                    }
                }
            }
        }
    }

    public CharacterProperties Properties {
        get {
            return properties;
        }
    }
    public CharacterController Controller {
        get {
            return controller;
        }
    }


    public Character (Transform characterTransform) {
        this.transform = characterTransform;
        this.animation = characterTransform.GetComponentInChildren<Animation> ();
        this.controller = characterTransform.GetComponent<CharacterController> ();
        this.properties = new CharacterProperties ();
    }


    public Character (Transform characterTransform, Animation animationIn, CharacterController controllerIn) {
        this.transform = characterTransform;
        this.properties = new CharacterProperties ();
        this.animation = animationIn;
        this.controller = controllerIn;
    }

    public void AddAction (string actionNameIn, Actions.ICharacterAction actionIn) {
        if (!availableActions.ContainsKey (actionNameIn))
            availableActions.Add (actionNameIn, actionIn);
        else
            throw new Exception ("Action has already been registered to character");
    }

    public Actions.ICharacterAction GetAction (string actionNameIn) {
        return availableActions[actionNameIn];
    }    
    public bool HasAnimation () {
        return this.animation != null;
    }
    public CharacterResource GetResource (CharacterResource.Type type) {
        return properties.GetResource (type);
    }
    public float GetDistance (Character other) {
        return Vector3.Distance (this.transform.position, other.transform.position);
    }
}
