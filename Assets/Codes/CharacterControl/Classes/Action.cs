using UnityEngine;

namespace Actions {
    public abstract class Action : IAction {
        private float startTime;
        private Phase actionPhase;
        private float duration;
        private Character user;

        public Action (Character characterIn, float durationIn) {
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

        public Character Character {
            get {
                return user;
            }
        }

        protected virtual void PreActions () {
            startTime = Time.time;
        }

        protected virtual void PostActions () {
        }

        protected abstract void PerformAction ();

        public Phase Execute () {    //Returns whether or not the action finished    
            if (actionPhase == Phase.NotActing) {
                PreActions ();
                actionPhase = Phase.Acting;
            }
            if (actionPhase == Phase.Acting) {
                PerformAction ();
                if (startTime + duration < Time.time) {
                    PostActions ();
                    actionPhase = Phase.NotActing;
                }
            }
            return actionPhase;            
        }
        public virtual bool IsPrimitive () {
            return false;
        }
    }
}   