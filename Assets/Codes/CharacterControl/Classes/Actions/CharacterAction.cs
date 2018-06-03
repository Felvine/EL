using UnityEngine;
using Znko.Events;
using Znko.Characters;
using System;

namespace Znko.Actions {
    public abstract class CharacterAction : ICharacterAction {
        //Constants
        public const float finishingPercent = 0.9f;
        public const float blendTime = 0.5f;


        // User and Animation
        private Character _user;
        private ResourceCost _cost;
        private AnimationClip _animationClip;
        private bool _disableAnimation = false;
        private ActionEvent[] _events;

        //Time management
        private float _startTime;
        private float _duration;

        protected int _priority = 0;

        private Phase _actionPhase;

        public CharacterAction (Character characterIn, float durationIn, AnimationClip animationClipIn, ResourceCost cost = null, params ActionEvent[] eventsIn) {
            this._actionPhase = Phase.NotActing;
            this._user = characterIn;
            this._duration = durationIn;
            this._animationClip = animationClipIn;
            this._events = eventsIn;
            this._cost = cost;
        }


        #region Fields
        protected Phase ActionPhase {
            get {
                return _actionPhase;
            }
        }

        protected float Duration {
            get {
                return _duration;
            }
        }

        public Character User {
            get {
                return _user;
            }
        }

        protected float StartTime {
            get {
                return _startTime;
            }
        }

        public int Priority {
            get {
                return _priority;
            }
        }

        public bool DisableAnimation {
            get {
                return _disableAnimation;
            }

            set {
                _disableAnimation = value;
            }
        }

        public ResourceCost Cost {
            get {
                return _cost;
            }
        }
        #endregion

        #region ICharacterAction interface methods
        public virtual void PreActions (ICharacterAction previousAction, ICharacterController controller) {
            _startTime = Time.time;
            if (controller.GetUser().HasEnoughResource(Cost))
            {
                controller.GetUser().ReduceResource(Cost);
            }
            else
            {
                throw new ArgumentException();
            }
            if (!DisableAnimation && HasAnimationClip () && User.HasAnimation ()) {
                User.Animation.CrossFade (_animationClip.name);
            }
            foreach (ActionEvent ae in _events)
            {
                if (ae.GetPhase() == ActionEvent.Phase.PreAction)
                    controller.AddEvent(ae.Value);
            }
        }

        public virtual void PostActions (ICharacterAction nextAction, ICharacterController controller) {
            foreach (ActionEvent ae in _events)
            {
                if (ae.GetPhase() == ActionEvent.Phase.PostAction)
                    controller.AddEvent(ae.Value);
            }
        }

        protected abstract void PerformAction ();

        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction, ICharacterController controller) {    //Returns whether or not the action finished    
            if (_actionPhase == Phase.NotActing) {
                PreActions (previousAction, controller);
                _actionPhase = Phase.Acting;
            }
            if (_actionPhase == Phase.Acting) {
                PerformAction ();
                if (_startTime + _duration < Time.time) {
                    PostActions (nextAction, controller);
                    _actionPhase = Phase.NotActing;
                }
            }
            return _actionPhase;            
        }

        public bool IsFinishing () {
            if (_priority == 0)
                return true;
            else
                return (_startTime + (_duration * finishingPercent)) < Time.time;
        }
#endregion
        public bool HasAnimationClip () {
            return this._animationClip != null;
        }

        public float GetDuration () {
            return Duration;
        }
        public override string ToString () {
            if (_animationClip != null)
                return this._animationClip.name;
            else
                return " - ";
        }


        public static bool CanInterrupt (ICharacterAction currentAction, ICharacterAction nextAction)
        {
            if (currentAction == null || nextAction == null)
                return false;
            return (/*currentAction.Priority == 0 ||*/ nextAction.Priority > currentAction.Priority);
        }
    }
}   