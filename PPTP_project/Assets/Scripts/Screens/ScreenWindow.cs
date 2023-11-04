using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens{

    public class ScreenWindow : MonoBehaviour {
        public bool isAnimRun;
        public GameObject myScreen;
        public GameScreens myTypeScreen;
        public bool paramtersAreSet;

        public virtual void SetParameters() { }

        public virtual void ResetPositions() { }

        public virtual void Show() {
            SetParameters();
        }

        public virtual void Hide() {
            ResetPositions();
        }

    }
}
