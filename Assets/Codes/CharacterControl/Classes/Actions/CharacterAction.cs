using UnityEngine;

namespace Actions {
    public abstract class CharacterAction : ICharacterAction {
        private const float finishingPercent = 0.9f;
        protected int priority = 0;
        private float startTime;
        private Phase actionPhase;
        private float duration;
        private Character user;
        AnimationClip animationClip;
        protected float blendTime = 0.5f;

        public CharacterAction (AnimatedCharacter characterIn, float durationIn, AnimationClip animationClipIn) {
            this.actionPhase = Phase.NotActing;
            this.user = characterIn;
            this.duration = durationIn;
            this.animationClip = animationClipIn;
        }

        protected Phase ActionPhase {
            get {
                return actionPhase;
            }
        }

        protected float Duration {
            get {
                return duration;
            }
        }

        public Character User {
            get {
                return user;
            }
        }

        protected float StartTime {
            get {
                return startTime;
            }
        }

        public int Priority {
            get {
                return priority;
            }
        }

        public virtual void PreActions (ICharacterAction previousAction) {
            if(previousAction != null)
                startTime = Time.time;
            if (animationClip != null) {
                Debug.Log (animationClip.name);
                ((AnimatedCharacter)User).Animaton.CrossFade (animationClip.name);
            }
        }

        public virtual void PostActions (ICharacterAction nextAction) {
        }

        protected abstract void PerformAction ();

        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction) {    //Returns whether or not the action finished    
            if (actionPhase == Phase.NotActing) {
                PreActions (previousAction);
                actionPhase = Phase.Acting;
            }
            if (actionPhase == Phase.Acting) {
                PerformAction ();
                if (startTime + duration < Time.time) {
                    PostActions (nextAction);
                    actionPhase = Phase.NotActing;
                }
            }
            return actionPhase;            
        }

        public bool IsFinishing () {
            if (priority == 0)
                return true;
            else
                return startTime + duration * finishingPercent > Time.time;
        }
    }
}   