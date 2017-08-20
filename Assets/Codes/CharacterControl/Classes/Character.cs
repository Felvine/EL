using System;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    enum HorizontalDirection { West, East}

    private Vector3 direction = Vector3.zero;
    private HorizontalDirection horizontalDir = HorizontalDirection.East;

    private CharacterProperties properties;
    private Dictionary<string, Actions.ICharacterAction> availableActions = new Dictionary<string, Actions.ICharacterAction> ();

    private CharacterController controller;
    private Transform transform;
    private Animator animations;

    public Vector3 Direction {
        get {
            return direction;
        }
        set {
            Debug.Log ("Dir");
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

    public CharacterController Controller {
        get {
            return controller;
        }
    }

    public Transform Transform {
        get {
            return transform;
        }
    }

    public Animator Animations {
        get {
            return animations;
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

    public Character (CharacterController characterController, Transform characterTransform, Animator animatorIn){
        this.controller = characterController;
        this.transform = characterTransform;
        this.animations = animatorIn;
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
}
