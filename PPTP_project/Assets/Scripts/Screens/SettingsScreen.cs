using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens {
    public class SettingsScreen : ScreenWindow {
        
        //Screen components
        public RectTransform title;

//**********SHOW SCREEN**********// 

        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimRun = true;

            yield return new WaitForEndOfFrame();

            isAnimRun = false;
        }

//**********HIDE SCREEN**********//

        public override void Hide(){
            StartCoroutine(Co_Hide());
            base.Hide();
        }
        IEnumerator Co_Hide(){
            isAnimRun = true;
            yield return new WaitForEndOfFrame();
            ResetPositions();
            isAnimRun = false;
        }
    }
}