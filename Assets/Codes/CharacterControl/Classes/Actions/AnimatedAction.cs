using UnityEngine;
namespace Actions {
    abstract class AnimatedAction : CharacterAction {
        AnimationClip animationClip;
        protected float blendTime = 0.5f;
        public AnimatedAction (AnimatedCharacter characterIn, float durationIn, AnimationClip animationClipIn) : base (characterIn, durationIn) {
            this.animationClip = animationClipIn;
        }

        public override void PreActions (ICharacterAction previousAction) {
            //Debug.Log (animationClip.name);
            base.PreActions (previousAction);

            ((AnimatedCharacter)User).Animaton.CrossFade (animationClip.name);

            //if (previousAction == this)
            //    ((AnimatedCharacter)User).Animaton.Blend (animationClip.name, 0.5f, blendTime);
            //else
            //    ((AnimatedCharacter)User).Animaton.Play (animationClip.name);
        }

        public override void PostActions (ICharacterAction nextAction) {
            base.PostActions (nextAction);

        }
    }
}
