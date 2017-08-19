using System;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    Vector3 direction;
    CharacterProperties properties;
    CharacterController controller;
    Dictionary<string, Actions.IAction> availableActions = new Dictionary<string, Actions.IAction> ();
    Transform transform;

    public Vector3 Direction {
        get {
            return direction;
        }
        set {
            direction = value;
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

    public Character (CharacterController characterController, Transform characterTransform){
        this.controller = characterController;
        this.transform = characterTransform;
    }

    public void AddAction (string actionNameIn, Actions.IAction actionIn) {
        if (!availableActions.ContainsKey (actionNameIn))
            availableActions.Add (actionNameIn, actionIn);
        else
            throw new Exception ("Action has already been registered to character");
    }

    public Actions.IAction GetAction (string actionNameIn) {
        return availableActions[actionNameIn];
    }


    
}
