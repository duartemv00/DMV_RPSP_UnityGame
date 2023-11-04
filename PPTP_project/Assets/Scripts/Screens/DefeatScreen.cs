using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens {
    public class DefeatScreen : ScreenWindow {
        
        //Screen components
        public RectTransform screen;

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

            screen.GetComponent<CanvasGroup>().alpha = 0;

            while(screen.GetComponent<CanvasGroup>().alpha <= 1){
                screen.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            isAnimRun = false;
        }

//**********HIDE SCREEN**********//

        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            isAnimRun = true;
            yield return new WaitForEndOfFrame();
            ResetPositions();
            isAnimRun = false;
        }
    }
}