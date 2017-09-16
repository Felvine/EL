using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actions {
    public class CharacterActionSequence : ICharacterAction {
        // Constants
        public const float finishingPercent = 0.9f;
        public const float blendTime = 0.5f;
        private const bool uniformAnimation = true;

        // User and Animation
        private Character user;
        private AnimationClip animationClip;
        private bool disableAnimation = false;

        // Time management
        private float startTime;
        private float duration;
        
        protected int priority = 0;

        private Phase actionPhase;

        //Action list and currentAction
        private List<ICharacterAction> actions;
        private int step = 0;

        public bool DisableAnimation {
            get {
                return disableAnimation;
            }

            set {
                disableAnimation = value;
            }
        }

        public Character User {
            get {
                return user;
            }

            set {
                user = value;
            }
        }

        public int Priority {
            get {
                return priority;
            }
        }

        public CharacterActionSequence (Character userIn, AnimationClip animationClipIn, params ICharacterAction[] actionsIn) {
            this.User = userIn;
            this.actionPhase = Phase.NotActing;
            this.animationClip = animationClipIn;
            this.priority = 1;
            this.actions = new List<ICharacterAction> ();
            this.duration = 0;
            step = 0;
            for (int i = 0; i < actionsIn.Length; i++) {
                this.duration =+ actionsIn[i].GetDuration ();
                this.actions.Add (actionsIn[i]);
            }
        }
        public virtual void PreActions (ICharacterAction previousAction) {
            if (previousAction != null)
                startTime = Time.time;
            if (!DisableAnimation && HasAnimationClip () && User.HasAnimation ()) {
                User.Animaton.CrossFade (animationClip.name);
            }
        }

        public virtual void PostActions (ICharacterAction nextAction) {
        }


        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction) {
            if (actionPhase == Phase.NotActing) {
                PreActions (previousAction);
                actionPhase = Phase.Acting;
            }
            if (actionPhase == Phase.Acting) {
                if (step == (actions.Count - 1)) {
                    if (actions[step].Execute (previousAction, nextAction) == Phase.NotActing) {
                        step = 0;
                        PostActions (nextAction);
                        actionPhase = Phase.NotActing;
                    } else {
                        actionPhase = Phase.Acting;
                    }
                } else {
                    if (actions[step].Execute (previousAction, nextAction) == Phase.NotActing)
                        step++;
                    actionPhase = Phase.Acting;
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

        public bool HasAnimationClip () {
            return this.animationClip != null;
        }

        public float GetDuration () {
            return duration;
        }
    }
}
