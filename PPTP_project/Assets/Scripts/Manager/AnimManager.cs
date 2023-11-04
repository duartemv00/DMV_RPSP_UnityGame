using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Duarto.Utilities;

namespace Duarto.PPTP.Manager{
    public class AnimManager : Singleton<AnimManager> {

        public GameObject enemyCam_GO;
        public CamShake playerCam_camShake;
        public GameObject corteImg_GO;
        public GameObject damageForeground_GO;
        public GameObject damageText_GO;
        public GameObject parryText_GO;
        public List<Sprite> enemyDamageSprite_List;
        public List<Sprite> enemyDeadSprite_List;
        [System.NonSerialized] public bool parry = false;

        public GameObject piedra;
        public GameObject papel;
        public GameObject tijera;
        public GameObject enemyElection_GO;

//**********DAMAGE ENEMY ANIMATION**********//
        public IEnumerator Co_DamageEnemy(){
            if(parry){ StartCoroutine(Co_ParryText()); parry=false; }
            StartCoroutine(Co_DamageText());
            GameManager.Instance.enemyHealth -= GameManager.Instance.streak+1;
            GameManager.Instance.enemyHPSlider.SetHealth(GameManager.Instance.enemyHealth);
            //Set enemy sprite to Damage/Dead
            if(GameManager.Instance.enemyHealth > 0){
                enemyCam_GO.GetComponent<Image>().sprite = enemyDamageSprite_List[GameManager.Instance.kills];
            } else { enemyCam_GO.GetComponent<Image>().sprite = enemyDeadSprite_List[GameManager.Instance.kills]; }
            AudioManager.Instance.PlayHit();
            enemyCam_GO.GetComponent<CamShake>().ShakeCamera(); //ENEMY CAMERA SHAKE
            //Cut animation
            while(corteImg_GO.GetComponent<CanvasGroup>().alpha < 1){
                corteImg_GO.GetComponent<CanvasGroup>().alpha += Time.deltaTime*4;
                yield return new WaitForEndOfFrame();
            }
            while(corteImg_GO.GetComponent<CanvasGroup>().alpha > 0) {
                corteImg_GO.GetComponent<CanvasGroup>().alpha -= Time.deltaTime*2;
                yield return new WaitForEndOfFrame();
            }
            if(GameManager.Instance.enemyHealth > 0){
                enemyCam_GO.GetComponent<Image>().sprite = GameManager.Instance.enemySprite_List[GameManager.Instance.kills];
            }    
        }

//**********DAMAGE PLAYER ANIMATION**********//
        public IEnumerator Co_DamagePlayer(){
            AudioManager.Instance.PlayDamage();
            GameManager.Instance.playerHealth -= 1;
            GameManager.Instance.playerHPSlider.SetHealth(GameManager.Instance.playerHealth);
            playerCam_camShake.ShakeCamera(); //PLAYER CAMERA SHAKE
            damageForeground_GO.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            damageForeground_GO.gameObject.SetActive(false);
        }

//**********ENEMY GO**********//
        public IEnumerator Co_EnemySwap(){
            Vector2 enemyAnchor = enemyCam_GO.GetComponent<RectTransform>().anchoredPosition;
            enemyCam_GO.GetComponent<RectTransform>().DOAnchorPosY(Screen.height*4,0.8f).OnComplete(()=>{
                GameManager.Instance.CreateEnemy(GameManager.Instance.kills);
                GameManager.Instance.GameReset();
                enemyCam_GO.GetComponent<RectTransform>().DOAnchorPosY(enemyAnchor.y,0.3f);  
            });
            yield return new WaitForEndOfFrame();
            
               
        }

//**********PARRY TEXT**********//
        public IEnumerator Co_ParryText(){
            parryText_GO.SetActive(true);
            yield return new WaitForSeconds(1);
            parryText_GO.SetActive(false);
        }

//**********DAMAGE TEXT**********//
        public IEnumerator Co_DamageText(){
            damageText_GO.GetComponent<TextMeshProUGUI>().text = "-"+(GameManager.Instance.streak+1).ToString();
            damageText_GO.SetActive(true);
            yield return new WaitForSeconds(1);
            damageText_GO.SetActive(false);
            
        }

//**********BUTTONS COOLDOWN**********//
        public IEnumerator Co_ButtonCooldown(){
            FillUnfillButtons(piedra, 1);
            FillUnfillButtons(papel, 1);
            FillUnfillButtons(tijera, 1);
            while(enemyElection_GO.GetComponent<Image>().fillAmount > 0) {
                enemyElection_GO.GetComponent<Image>().fillAmount -= 1/(60*GameManager.Instance.timeToParry);
                if(GameManager.Instance.player_Election != Elections.Rock){
                    piedra.GetComponent<Image>().fillAmount -= 1/(60*GameManager.Instance.timeToParry);
                }
                if(GameManager.Instance.player_Election != Elections.Paper){
                    papel.GetComponent<Image>().fillAmount -= 1/(60*GameManager.Instance.timeToParry);
                }
                if(GameManager.Instance.player_Election != Elections.Scissors){
                    tijera.GetComponent<Image>().fillAmount -= 1/(60*GameManager.Instance.timeToParry);
                }    
                yield return new WaitForEndOfFrame();
            }
            FillUnfillButtons(piedra, 1);
            FillUnfillButtons(papel, 1);
            FillUnfillButtons(tijera, 1);
            enemyElection_GO.GetComponent<Image>().fillAmount = 1;
        }
        public void FillUnfillButtons(GameObject go, int i){
            go.GetComponent<Image>().fillAmount = i;
        }

//**********ENEMY PARRY ANIM**********//
        public IEnumerator Co_enemyParryAnim(GameObject goAnim){
            goAnim.GetComponent<RectTransform>().DOSizeDelta(new Vector2 (200f,200f), 0f, false);
            goAnim.GetComponent<RectTransform>().DOSizeDelta(new Vector2 (300f,300f), 0.3f, false);
            yield return new WaitForEndOfFrame();
        }
    }
}
