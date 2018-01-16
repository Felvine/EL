using UnityEngine;
using Znko.Events;

namespace Znko.Actions {
    public abstract class CharacterAction : ICharacterAction {
        //Constants
        public const float finishingPercent = 0.9f;
        public const float blendTime = 0.5f;


        // User and Animation
        private Character user;
        private AnimationClip animationClip;
        private bool disableAnimation = false;
        private ActionEvent[] events;

        //Time management
        private float startTime;
        private float duration;

        protected int priority = 0;

        private Phase actionPhase;

        public CharacterAction (Character characterIn, float durationIn, AnimationClip animationClipIn, params ActionEvent[] eventsIn) {
            this.actionPhase = Phase.NotActing;
            this.user = characterIn;
            this.duration = durationIn;
            this.animationClip = animationClipIn;
            this.events = eventsIn;
        }


        #region Fields
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

        public bool DisableAnimation {
            get {
                return disableAnimation;
            }

            set {
                disableAnimation = value;
            }
        }
        #endregion

        #region ICharacterAction interface methods
        public virtual void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            startTime = Time.time;
            if (!DisableAnimation && HasAnimationClip () && User.HasAnimation ()) {
                User.Animation.CrossFade (animationClip.name);
            }
            foreach (ActionEvent ae in events)
            {
                if (ae.GetPhase() == ActionEvent.Phase.PreAction)
                    controller.AddEvent(ae.Value);
            }
        }

        public virtual void PostActions (ICharacterAction nextAction, ICharacterController controller) {
        }

        protected abstract void PerformAction ();

        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction, ICharacterController controller) {    //Returns whether or not the action finished    
            if (actionPhase == Phase.NotActing) {
                PreActions (previousAction, controller);
                actionPhase = Phase.Acting;
            }
            if (actionPhase == Phase.Acting) {
                PerformAction ();
                if (startTime + duration < Time.time) {
                    PostActions (nextAction, controller);
                    actionPhase = Phase.NotActing;
                }
            }
            return actionPhase;            
        }

        public bool IsFinishing () {
            if (priority == 0)
                return true;
            else
                return (startTime + (duration * finishingPercent)) < Time.time;
        }
#endregion
        public bool HasAnimationClip () {
            return this.animationClip != null;
        }

        public float GetDuration () {
            return Duration;
        }
        public override string ToString () {
            return this.animationClip.name;
        }


        public static bool CanInterrupt (ICharacterAction currentAction, ICharacterAction nextAction)
        {
            if (currentAction == null || nextAction == null)
                return false;
            return (currentAction.Priority == 0 || nextAction.Priority > currentAction.Priority);
        }
    }
}   