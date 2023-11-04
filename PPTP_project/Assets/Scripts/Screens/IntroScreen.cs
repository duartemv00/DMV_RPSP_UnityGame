using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens {
    public class IntroScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform logoDuarto0;
        public RectTransform text;

//******************************************************************************************************************************************// 
        public override void ResetPositions(){
            base.ResetPositions();
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimRun = true;

            //cambiar posiciones y alpha del logo y titulo
            logoDuarto0.GetComponent<CanvasGroup>().alpha = 0;
            text.GetComponent<CanvasGroup>().alpha = 0;

            yield return new WaitForSeconds(1);

            //LOGO DUARTO0
            while(logoDuarto0.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                logoDuarto0.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                text.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }
            yield return new WaitForSeconds(1);
            while(logoDuarto0.GetComponent<CanvasGroup>().alpha > 0) {
                yield return new WaitForEndOfFrame();
                logoDuarto0.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
                text.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            }

            ScreenManager.Instance.ChangeScreen(GameScreens.MainMenu,myTypeScreen);
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            yield return new WaitForEndOfFrame();
            isAnimRun = false;
            myScreen.SetActive(false);
        }
    }
}