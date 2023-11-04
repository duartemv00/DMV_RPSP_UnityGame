using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;
using Duarto.PPTP.Screens;

namespace Duarto.PPTP.Manager {

public enum GameScreens {None,Intro,MainMenu,Settings,Game,Pause,End,Win}

    public class ScreenManager : Singleton<ScreenManager> {
        
        public GameScreens typeScreen;
        public GameObject raycastShield; //Shield to prevent clics on screen
        public List<ScreenWindow> screens = new List<ScreenWindow>(); //List of all screens

        void Awake(){ //AWAKE
            foreach(ScreenWindow i in screens) { //deactivate all screens at the begining
                i.gameObject.SetActive(false);
            }
        }

//**********CHANGE SCREEN**********//
        public void ChangeScreen(GameScreens newScreen, GameScreens oldScreen){
            StartCoroutine(WaitBeforeSwapScreen(newScreen, oldScreen));
        }
        IEnumerator WaitBeforeSwapScreen(GameScreens newScreen, GameScreens oldScreen){
            raycastShield.SetActive(true);

            ScreenWindow newScreenGO = GetScreen(newScreen);
            ScreenWindow oldScreenGO = GetScreen(oldScreen);

            if(oldScreenGO != null) {
                oldScreenGO.Hide();
                while(oldScreenGO.isAnimRun){
                    yield return new WaitForEndOfFrame();
                }
                oldScreenGO.gameObject.SetActive(false);
            }

            
            if(newScreenGO != null) {
                newScreenGO.gameObject.SetActive(true);
                newScreenGO.Show();
                while(newScreenGO.isAnimRun){
                    yield return new WaitForEndOfFrame();
                }
            }
            raycastShield.SetActive(false);
        }

//**********INVERSE CHANGE SCREEN**********//
        public void InverseChangeScreen(GameScreens newScreen, GameScreens oldScreen){
            StartCoroutine(WaitBeforeSwapScreenInversed(newScreen, oldScreen));
        }
        IEnumerator WaitBeforeSwapScreenInversed(GameScreens newScreen, GameScreens oldScreen){
            //raycastShield.SetActive(true);

            ScreenWindow newScreenGO = GetScreen(newScreen);
            ScreenWindow oldScreenGO = GetScreen(oldScreen);

            if(newScreenGO != null) {
                Debug.Log("Pantalla nueva viene");
                newScreenGO.gameObject.SetActive(true);
                newScreenGO.Show();
                while(newScreenGO.isAnimRun){
                    yield return new WaitForEndOfFrame();
                }
            }

            if(oldScreenGO != null) {
                Debug.Log("Pantalla vieja se va yendo");
                oldScreenGO.Hide();
                while(oldScreenGO.isAnimRun){
                    yield return new WaitForEndOfFrame();
                }
                oldScreenGO.gameObject.SetActive(false);
            }
            
            //raycastShield.SetActive(false);
        }

//**********ADD SCREEN**********//
        public void AddScreen(GameScreens newScreen){
            StartCoroutine(WaitBeforeAddScreen(newScreen));
        }
        IEnumerator WaitBeforeAddScreen (GameScreens newScreen) {
            //raycastShield.SetActive(true);
            ScreenWindow newScreenGO = GetScreen(newScreen);
            if(newScreenGO != null) {
                newScreenGO.gameObject.SetActive(true);
                newScreenGO.Show();
                while (newScreenGO.isAnimRun) {
                    yield return new WaitForEndOfFrame();
                }
            }
            raycastShield.SetActive(false);
        }

//**********REMOVE SCREEN**********//
        public void RemoveScreen(GameScreens oldScreen){
            StartCoroutine(WaitBeforeRemoveScreen(oldScreen));
        }
        IEnumerator WaitBeforeRemoveScreen(GameScreens oldScreen){
            raycastShield.SetActive(true);
            ScreenWindow oldScreenGO = GetScreen(oldScreen);
            if(oldScreenGO != null) {
                oldScreenGO.Hide();
                while (oldScreenGO.isAnimRun) {
                    yield return new WaitForEndOfFrame();
                }
                oldScreenGO.gameObject.SetActive(false);
            }
            raycastShield.SetActive(false);
        }




        ScreenWindow GetScreen(GameScreens typeScreen){
            foreach(ScreenWindow i in screens){
                if(typeScreen == i.myTypeScreen) {
                    return i;
                }
            }
            return null;
        }
    }
}