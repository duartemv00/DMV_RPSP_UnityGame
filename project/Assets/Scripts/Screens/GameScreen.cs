using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens {
    public class GameScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform enemy;
        public RectTransform player;

        Vector2 enemyAnchor;
        Vector2 playerAnchor;

//**********// 

        public override void SetParameters(){ //stablish initial positions
            enemyAnchor = enemy.anchoredPosition;
            playerAnchor = player.anchoredPosition;
        }
        public override void ResetPositions(){
            enemy.anchoredPosition = enemyAnchor;
            player.anchoredPosition = playerAnchor;
        }

//**********SHOW SCREEN LOGIC**********// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimRun = true;

            enemy.anchoredPosition = new Vector2(enemyAnchor.x, enemyAnchor.y + 2*Screen.height);
            player.anchoredPosition = new Vector2(playerAnchor.x-2*Screen.width, playerAnchor.y);
            yield return new WaitForEndOfFrame();

            player.DOAnchorPosX(playerAnchor.x,0.5f);
            yield return new WaitForSeconds(0.5f);
            enemy.DOAnchorPosY(enemyAnchor.y,0.8f);

            isAnimRun = false;
        }

//**********HIDE SCREEN LOGIC**********// 
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