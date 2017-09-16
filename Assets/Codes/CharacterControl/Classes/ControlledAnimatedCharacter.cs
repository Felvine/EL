using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControlledAnimatedCharacter : AnimatedCharacter {
    private CharacterController controller;
    public CharacterController Controller {
        get {
            return controller;
        }
    }
    public ControlledAnimatedCharacter (Transform characterTransform, Animation animationIn, CharacterController controllerIn) : base (characterTransform, animationIn){
        this.controller = controllerIn;
    }

}