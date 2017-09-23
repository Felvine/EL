using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControlledCharacter : Character {
    private CharacterController controller;
    public CharacterController Controller {
        get {
            return controller;
        }
    }
    public ControlledCharacter (Transform characterTransform, Animation animationIn, CharacterController controllerIn) : base (characterTransform, animationIn){
        this.controller = controllerIn;
    }

}