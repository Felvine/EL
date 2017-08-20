using UnityEngine;
namespace Actions {
    abstract class AnimatedAction : CharacterAction {
        AnimationClip animationClip;
        public AnimatedAction (AnimatedCharacter characterIn, float durationIn, AnimationClip animationClipIn) : base (characterIn, durationIn) {
            this.animationClip = animationClipIn;
        }

        public override void PreActions (ICharacterAction previousAction) {
            base.PreActions (previousAction);
            ((AnimatedCharacter)User).Animaton.Play (animationClip.name);
            Debug.Log (animationClip.name);
        }
    }
}
