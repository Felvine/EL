using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Znko.Characters;

namespace Znko.Actions {
    public class CharacterActionSequence : ICharacterAction {
        // Constants
        public const float finishingPercent = 0.9f;
        public const float blendTime = 0.5f;

        // User and Animation
        private Character user;
        private AnimationClip animationClip;
        private bool disableAnimation = false;

        // Time management
        private float startTime;
        private float duration;

        private ResourceCost _cost;
      

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
                return actions.Max (r => r.Priority);
            }
        }

        public int Step {
            get {
                return step;
            }
        }

        public List<ICharacterAction> Actions {
            get {
                return actions;
            }
        }

        public ResourceCost Cost {
            get {
                return _cost;
            }
        }

        public CharacterActionSequence (Character userIn, AnimationClip animationClipIn, ResourceCost cost = null, params ICharacterAction[] actionsIn) {
            this.User = userIn;
            this.actionPhase = Phase.NotActing;
            this.animationClip = animationClipIn;
            this.actions = new List<ICharacterAction> ();
            this.duration = 0;
            step = 0;
            for (int i = 0; i < actionsIn.Length; i++) {
                this.duration =+ actionsIn[i].GetDuration ();
                this.actions.Add (actionsIn[i]);
            }
            _cost = cost;
        }
        public virtual void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            if (controller.GetUser().HasEnoughResource(Cost))
            {
                controller.GetUser().ReduceResource(Cost);
            }
            startTime = Time.time;
            if (!DisableAnimation && HasAnimationClip () && User.HasAnimation ()) {
                User.Animation.CrossFade (animationClip.name);
            }
        }

        public virtual void PostActions (ICharacterAction nextAction, ICharacterController controller) {
        }


        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction, ICharacterController controller) {
            if (actionPhase == Phase.NotActing) {
                PreActions (GetPreviousAction (previousAction), controller);
                actionPhase = Phase.Acting;
            }
            if (actionPhase == Phase.Acting) {
                if (step == (actions.Count - 1)) {
                    if (actions[step].Execute (GetPreviousAction (previousAction), GetNextAction (nextAction), controller) == Phase.NotActing) {
                        step = 0;
                        PostActions (GetNextAction (nextAction), controller);
                        actionPhase = Phase.NotActing;
                    } else {
                        actionPhase = Phase.Acting;
                    }
                } else {
                    if (actions[step].Execute (GetPreviousAction (previousAction), GetNextAction (nextAction), controller) == Phase.NotActing)
                        step++;
                    actionPhase = Phase.Acting;
                }
            }
            return actionPhase;
        }

        public bool IsFinishing () {
            if (Priority == 0)
                return true;
            if (step < actions.Count - 1)
                return false;
            else
                return actions[step].IsFinishing();

        }

        public bool HasAnimationClip () {
            return this.animationClip != null;
        }

        public float GetDuration () {
            return duration;
        }

        public ICharacterAction GetAction (int stepIn) {
            if (stepIn < actions.Count && stepIn >= 0)
                return actions[stepIn];
            else
                return null;
        }

        private ICharacterAction GetPreviousAction (ICharacterAction beforeSequence) {
            ICharacterAction previousAction = GetAction (step - 1);
            if (previousAction == null)
                return beforeSequence;
            else
                return previousAction;
        }

        private ICharacterAction GetNextAction (ICharacterAction afterSequence) {
            ICharacterAction nextAction = GetAction (step + 1);
            if (nextAction == null)
                return afterSequence;
            else
                return nextAction;
        }

        public bool CanInterrupt(ICharacterAction currentAction)
        {
            return false;
        }

        public override string ToString()
        {
            string result = "Action Sequence ";
            foreach (ICharacterAction action in this.actions)
            {
                result = result + action.ToString();
            }
            return result;
        }
    }
}
