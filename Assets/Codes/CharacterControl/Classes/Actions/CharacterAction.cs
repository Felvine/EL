using UnityEngine;

namespace Actions {
    public abstract class CharacterAction : ICharacterAction {
        private float startTime;
        private Phase actionPhase;
        private float duration;
        private Character user;

        public CharacterAction (Character characterIn, float durationIn) {
            this.actionPhase = Phase.NotActing;
            this.user = characterIn;
            this.duration = durationIn;
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

        public virtual void PreActions (ICharacterAction previousAction) {
            if(previousAction != null)
            startTime = Time.time;
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
    }
}   