using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.PPTP.Manager;

namespace Duarto.PPTP.Screens {
    public class MainMenuScreen : ScreenWindow {
        
        //Screen components
        public RectTransform rock;
        public RectTransform paper;
        public RectTransform scissors;
        public RectTransform playBtn;
        public RectTransform settingsBtn;
        public RectTransform exitBtn;

        //Screen component anchor
        Vector2 rockAnchor;
        Vector2 paperAnchor;
        Vector2 scissorsAnchor;
        Vector2 playBtnAnchor;
        Vector2 settingsBtnAnchor;
        Vector2 exitAnchor;

//********************// 

        public override void SetParameters(){
            //stablish initial positions
            playBtnAnchor = playBtn.anchoredPosition;
            settingsBtnAnchor = settingsBtn.anchoredPosition;
            exitAnchor = exitBtn.anchoredPosition;
            rockAnchor = rock.anchoredPosition;
            paperAnchor = paper.anchoredPosition;
            scissorsAnchor = scissors.anchoredPosition;
        }
        public override void ResetPositions(){
            playBtn.anchoredPosition = playBtnAnchor;
            settingsBtn.anchoredPosition = settingsBtnAnchor;
            exitBtn.anchoredPosition = exitAnchor;
            rock.anchoredPosition = rockAnchor;
            paper.anchoredPosition = paperAnchor;
            scissors.anchoredPosition = scissorsAnchor;
        }

        public void StartGame(){
            ScreenManager.Instance.ChangeScreen(GameScreens.Game, myTypeScreen);
        }

//**********SHOW SCREEN**********// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimRun = true;

            rock.anchoredPosition = new Vector2(rockAnchor.x,rockAnchor.y-Screen.height*2);
            paper.anchoredPosition = new Vector2(paperAnchor.x,paperAnchor.y-Screen.height*2);
            scissors.anchoredPosition = new Vector2(scissorsAnchor.x,scissorsAnchor.y-Screen.height*2);
            playBtn.anchoredPosition = new Vector2(playBtnAnchor.x - Screen.width*3,playBtnAnchor.y);
            settingsBtn.anchoredPosition = new Vector2(settingsBtnAnchor.x,settingsBtnAnchor.y+Screen.height*2);
            exitBtn.anchoredPosition = new Vector2(exitAnchor.x,exitAnchor.y+Screen.height*2);

            yield return new WaitForEndOfFrame();

            rock.DOAnchorPosY(rockAnchor.y,0.5f);
            paper.DOAnchorPosY(paperAnchor.y,0.5f);
            scissors.DOAnchorPosY(scissorsAnchor.y,0.5f);
            playBtn.DOAnchorPosX(playBtnAnchor.x,0.5f);
            settingsBtn.DOAnchorPosY(settingsBtnAnchor.y,0.5f);
            exitBtn.DOAnchorPosY(exitAnchor.y,0.5f);

            StartCoroutine(Co_ScreenRockAnim());
            StartCoroutine(Co_ScreenPaperAnim());
            StartCoroutine(Co_ScreenScissorsAnim());

            isAnimRun = false;
        }

        IEnumerator Co_ScreenRockAnim(){
            while(true){
                while(rock.anchoredPosition.y<(rockAnchor.y+10)){
                    rock.anchoredPosition = new Vector2(rockAnchor.x,rock.anchoredPosition.y+0.4f);
                    yield return new WaitForEndOfFrame();
                }
                while(rock.anchoredPosition.y>(rockAnchor.y-10)){
                    rock.anchoredPosition = new Vector2(rockAnchor.x,rock.anchoredPosition.y-0.4f);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        IEnumerator Co_ScreenPaperAnim(){
            while(true){
                while(paper.anchoredPosition.y<(paperAnchor.y+5)){
                    paper.anchoredPosition = new Vector2(paperAnchor.x,paper.anchoredPosition.y+0.3f);
                    yield return new WaitForEndOfFrame();
                }
                while(paper.anchoredPosition.y>(paperAnchor.y-15)){
                    paper.anchoredPosition = new Vector2(paperAnchor.x,paper.anchoredPosition.y-0.3f);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        
        IEnumerator Co_ScreenScissorsAnim(){
            while(true){
                while(scissors.anchoredPosition.y<(scissorsAnchor.y+15)){
                    scissors.anchoredPosition = new Vector2(scissorsAnchor.x,scissors.anchoredPosition.y+0.2f);
                    yield return new WaitForEndOfFrame();
                }
                while(scissors.anchoredPosition.y>(scissorsAnchor.y-5)){
                    scissors.anchoredPosition = new Vector2(scissorsAnchor.x,scissors.anchoredPosition.y-0.2f);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

//**********HIDE SCREEN**********// 
        public override void Hide(){
            StopAllCoroutines();
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            isAnimRun = true;
            yield return new WaitForSeconds(0.01f);
            rock.DOAnchorPos(new Vector2(-210f,-550f),0.5f);
            paper.DOAnchorPos(new Vector2(0f,-550f),0.5f);
            scissors.DOAnchorPos(new Vector2(210f,-550f),0.5f).OnComplete(()=>{
                isAnimRun = false;
            });
            
        }
    }
}