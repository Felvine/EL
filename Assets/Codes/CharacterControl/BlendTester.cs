using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Obsolete {
    class BlendTester : MonoBehaviour{
        public Animation animation;
        void Start () {
            this.animation = GetComponentInChildren<Animation> ();
        }
        void Update () {
            if (Input.GetKeyDown (KeyCode.Space)) {
                animation.CrossFadeQueued ("Player_Walk", 1f);
            }
            if (Input.GetKeyDown (KeyCode.R)) {
                animation.Play ("Player_Run");
            }
            if (Input.GetKeyDown (KeyCode.E)) {
                animation.Stop ();
            }
        }
    }
}