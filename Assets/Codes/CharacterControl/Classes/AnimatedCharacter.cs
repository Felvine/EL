using UnityEngine;

public class AnimatedCharacter : Character {
    private Animation animation;

    public Animation Animaton {
        get {
            return animation;
        }
    }

    public AnimatedCharacter (Transform characterTransform, Animation animationIn) : base (characterTransform){
        this.animation = animationIn;
    }

}
