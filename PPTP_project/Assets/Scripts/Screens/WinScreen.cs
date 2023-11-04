using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens {
    public class WinScreen : ScreenWindow {

//**********RESTART GAME**********// 

        public void RestartGame(){
            ScreenManager.Instance.ChangeScreen(GameScreens.Game, myTypeScreen);
        }

//**********SHOW SCREEN**********// 

        public override void Show(){
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
        }
        IEnumerator Co_Hide(){
            isAnimRun = true;
            yield return new WaitForEndOfFrame();
            isAnimRun = false;
        }
    }
}