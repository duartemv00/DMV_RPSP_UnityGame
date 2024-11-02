using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Duarto.Utilities;

namespace Duarto.PPTP.Manager{

public enum Elections {None,Rock,Paper,Scissors}

    public class GameManager : Singleton<GameManager> {
        bool selectTime = true;
        bool firstSelectionDone;
        bool firstSelectionDoneEnemy;

        public TextMeshProUGUI hint_Text;
        public GameObject countdownText_GO;
        public float timeToParry = 1;
        public List<Sprite> enemySprite_List;
        public List<Sprite> enemySpriteParry_List;
        public GameObject raycastShield;

        [Header("Streak")]
        public GameObject streak_GO;
        public TextMeshProUGUI streak_Text;
        [System.NonSerialized] public int streak;

        [System.NonSerialized] public int kills;
        
        [Header("Actions")]
        //Player
        public GameObject rockButton_GO;
        public GameObject paperButton_GO;
        public GameObject scissorsButton_GO;
        //Enemy
        public GameObject enemyElection_GO;
        public GameObject enemyElectionRock_GO;
        public GameObject enemyElectionPaper_GO;
        public GameObject enemyElectionScissors_GO;
        [System.NonSerialized] public Elections player_Election;
        Elections enemy_Election;

        [Header("Icons (Sprites)")]
        public Sprite rockBtn_sprite;
        public Sprite paperBtn_sprite;
        public Sprite scissorsBtn_sprite;
        public Sprite rockPressBtn_sprite;
        public Sprite paperPressBtn_sprite;
        public Sprite scissorsPressBtn_sprite;
        public Sprite blank_sprite;
            
        [Header("Health bars")]
        public HealthBarScript playerHPSlider;
        public HealthBarScript enemyHPSlider;
        public int playerHealth;
        [System.NonSerialized] public int enemyHealth;

//**********AWAKE**********// 
        void Awake(){
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Cursor.visible = true;
        }

//**********UPDATE**********// 
        void Update() {
            if(Application.targetFrameRate != 60) {
                Application.targetFrameRate = 60;
            }
        }

//**********START**********// 
        void Start(){
            ScreenManager.Instance.ChangeScreen(GameScreens.MainMenu,GameScreens.None);
        }

//**********PLAYER SELECTIONS**********// 
        public void SelectRock(){
            if(selectTime){
                Elections aux = player_Election;
                player_Election = Elections.Rock;
                //Button color change
                rockButton_GO.GetComponent<Image>().sprite = rockPressBtn_sprite;
                paperButton_GO.GetComponent<Image>().sprite = paperBtn_sprite;
                scissorsButton_GO.GetComponent<Image>().sprite = scissorsBtn_sprite;
                if(!firstSelectionDone){
                    firstSelectionDone = true;
                    StartCoroutine(Co_countdown());
                } else if (!(aux==player_Election)){
                    AnimManager.Instance.parry = true;
                }
                AnimManager.Instance.FillUnfillButtons(rockButton_GO, 1);
                AnimManager.Instance.FillUnfillButtons(paperButton_GO, 0);
                AnimManager.Instance.FillUnfillButtons(scissorsButton_GO, 0);
                selectTime = false;
                raycastShield.SetActive(true);
            }   
        }
        public void SelectPaper(){
            if(selectTime){
                Elections aux = player_Election;
                player_Election = Elections.Paper;
                //Button color change
                rockButton_GO.GetComponent<Image>().sprite = rockBtn_sprite;
                paperButton_GO.GetComponent<Image>().sprite = paperPressBtn_sprite;
                scissorsButton_GO.GetComponent<Image>().sprite = scissorsBtn_sprite;
                if(!firstSelectionDone){ 
                    firstSelectionDone = true;
                    StartCoroutine(Co_countdown());
                } else if (!(aux==player_Election)){
                    AnimManager.Instance.parry = true; 
                }
                AnimManager.Instance.FillUnfillButtons(rockButton_GO, 0);
                AnimManager.Instance.FillUnfillButtons(paperButton_GO, 1);
                AnimManager.Instance.FillUnfillButtons(scissorsButton_GO, 0);
                selectTime = false;
                raycastShield.SetActive(true);
            }
        }
        public void SelectScissors(){
            if(selectTime){
                Elections aux = player_Election;
                player_Election = Elections.Scissors;
                //Button color change
                rockButton_GO.GetComponent<Image>().sprite = rockBtn_sprite;
                paperButton_GO.GetComponent<Image>().sprite = paperBtn_sprite;
                scissorsButton_GO.GetComponent<Image>().sprite = scissorsPressBtn_sprite;
                if(!firstSelectionDone) {
                    firstSelectionDone = true;
                    StartCoroutine(Co_countdown());
                } else if (!(aux==player_Election)){
                    AnimManager.Instance.parry = true;          
                }
                AnimManager.Instance.FillUnfillButtons(rockButton_GO, 0);
                AnimManager.Instance.FillUnfillButtons(paperButton_GO, 0);
                AnimManager.Instance.FillUnfillButtons(scissorsButton_GO, 1);
                selectTime = false;
                raycastShield.SetActive(true);
            }
        }

//**********COUNTDOWN**********//
        IEnumerator Co_countdown(){
            enemyElection_GO.SetActive(true);
            hint_Text.gameObject.SetActive(false);
            float timmer = 3;
            countdownText_GO.SetActive(true);
            while(timmer>0) {
                timmer -= Time.deltaTime;
                countdownText_GO.GetComponent<TextMeshProUGUI>().text = PPTPResources.Instance.Round(timmer,0).ToString();
                yield return new WaitForEndOfFrame();
            } 
            countdownText_GO.SetActive(false);
            RandomElection();
            raycastShield.SetActive(false);
            StartCoroutine(Co_parryCountdown());
        }

//********PARRY TIME**********//
        IEnumerator Co_parryCountdown(){
            //Activate parry hint
            hint_Text.text = "Parry time!";
            StartCoroutine(AnimManager.Instance.Co_ButtonCooldown());
            hint_Text.gameObject.SetActive(true);
            selectTime = true; //Activate selection
            //Enemy second selection
            bool aux = CoinFlip();
            if(aux){
                AnimManager.Instance.enemyCam_GO.GetComponent<Image>().sprite = enemySpriteParry_List[kills];
            }
            yield return new WaitForSeconds(timeToParry/2);
            if(aux){
                RandomElection();
            }
            yield return new WaitForSeconds(timeToParry/2);
            raycastShield.SetActive(true);
            hint_Text.gameObject.SetActive(false); //Deactivate parry hint
            selectTime = false; //Deactivate selection
            CompareElections();
            yield return new WaitForSeconds(1);
            EndTurnCheck();
        }

        bool CoinFlip(){
            int random = Random.Range(0,2);
            if(random == 0) { return true; }
            else { return false; }
        }

//********ENEMY RANDOM ELECTION**********//
        public void RandomElection(){
            int random = Random.Range(0,3);
            switch(random){
                case 0:
                    if(enemy_Election != Elections.Rock){
                        enemy_Election = Elections.Rock; 
                        enemyElectionRock_GO.SetActive(true); 
                        enemyElectionPaper_GO.SetActive(false); 
                        enemyElectionScissors_GO.SetActive(false);
                        if(firstSelectionDoneEnemy) {
                            StartCoroutine(AnimManager.Instance.Co_enemyParryAnim(enemyElectionRock_GO));
                        } else { firstSelectionDoneEnemy = true; }
                        
                    } else {RandomElection();}
                    break;
                case 1:
                    if(enemy_Election != Elections.Paper) {
                        enemy_Election = Elections.Paper;
                        enemyElectionRock_GO.SetActive(false);
                        enemyElectionPaper_GO.SetActive(true); 
                        enemyElectionScissors_GO.SetActive(false);
                        if(firstSelectionDoneEnemy) {
                            StartCoroutine(AnimManager.Instance.Co_enemyParryAnim(enemyElectionPaper_GO));
                        } else {firstSelectionDoneEnemy = true; }
                    } else {RandomElection();}
                    break;
                    
                case 2:
                    if(enemy_Election != Elections.Scissors){
                        enemy_Election = Elections.Scissors; 
                        enemyElectionRock_GO.SetActive(false); 
                        enemyElectionPaper_GO.SetActive(false); 
                        enemyElectionScissors_GO.SetActive(true); 
                        if(firstSelectionDoneEnemy) {
                            StartCoroutine(AnimManager.Instance.Co_enemyParryAnim(enemyElectionScissors_GO));
                        } else {firstSelectionDoneEnemy = true; }
                    } else {RandomElection();}
                    break;    
            }
        }

//********COMPARE ELECTIONS**********//
        void CompareElections(){
            if(player_Election == Elections.Rock){
                if (enemy_Election == Elections.Scissors){
                    StartCoroutine(AnimManager.Instance.Co_DamageEnemy());
                    IncreaseStreak();
                } else if(enemy_Election == Elections.Paper){
                    StartCoroutine(AnimManager.Instance.Co_DamagePlayer());
                    StopStreak();
                } else {AudioManager.Instance.PlayEmpate();}

            } else if (player_Election == Elections.Paper){
                if (enemy_Election == Elections.Rock){
                    StartCoroutine(AnimManager.Instance.Co_DamageEnemy());
                    IncreaseStreak();
                } else if(enemy_Election == Elections.Scissors){ 
                    StartCoroutine(AnimManager.Instance.Co_DamagePlayer());
                    StopStreak();
                } else {AudioManager.Instance.PlayEmpate();}
            
            } else if (player_Election == Elections.Scissors) {
                if (enemy_Election == Elections.Paper){
                    StartCoroutine(AnimManager.Instance.Co_DamageEnemy());
                    IncreaseStreak();
                } else if(enemy_Election == Elections.Rock){ 
                    StartCoroutine(AnimManager.Instance.Co_DamagePlayer());
                    StopStreak();
                } else {AudioManager.Instance.PlayEmpate();}
            }
        }

        void IncreaseStreak(){
            streak += 1;
            streak_Text.text = "+"+streak.ToString();
            streak_GO.gameObject.SetActive(true);
        }

        void StopStreak(){
            streak = 0;
            streak_GO.gameObject.SetActive(false);
        }

//********END TURN CHECK**********//
//Decide what action to do next
        void EndTurnCheck(){
            raycastShield.SetActive(false);
            if(playerHealth == 0) { //GAME OVER
                GameOver();
            } else if(enemyHealth <= 0) { //NEW ENEMY
                NewRival();
            } else{ //NEXT TURN
                GameReset();
            }
        }

//********TURN RESET**********//
        public void GameReset(){
            //Hint text
            hint_Text.text = "Selecciona tu acción.";
            hint_Text.gameObject.SetActive(true);
            //Action buttons
            rockButton_GO.GetComponent<Image>().sprite = rockBtn_sprite;
            paperButton_GO.GetComponent<Image>().sprite = paperBtn_sprite;
            scissorsButton_GO.GetComponent<Image>().sprite = scissorsBtn_sprite;
            //Restart enemy
            AnimManager.Instance.enemyCam_GO.GetComponent<Image>().sprite = enemySprite_List[kills];
            enemyElection_GO.SetActive(false);
            enemyElectionRock_GO.SetActive(false);
            enemyElectionPaper_GO.SetActive(false);
            enemyElectionScissors_GO.SetActive(false);
            enemy_Election = Elections.None;
            //Gameplay state values
            selectTime = true;
            firstSelectionDone = false;
            firstSelectionDoneEnemy = false;
        }

//********GAME OVER**********//
        void GameOver(){
            //ScreenManager.Instance.InverseChangeScreen(GameScreens.End,GameScreens.Game); //Cambio de pantalla
            AudioManager.Instance.StopIngameMusic(); //Stop playing game music
            ScreenManager.Instance.AddScreen(GameScreens.End);
        }

//********NEW RIVAL**********//
        void NewRival(){
            kills += 1; //Increase kills
            if(kills >= 10){
                ScreenManager.Instance.ChangeScreen(GameScreens.Win, GameScreens.Game);
            } else {
                StartCoroutine(AnimManager.Instance.Co_EnemySwap()); //cambio de enemigo
                //Reset player health
                playerHealth = 3;
                playerHPSlider.SetHealth(3);
            }
        }

//********CREATE ENEMY**********//
        public void CreateEnemy(int kills){
            AnimManager.Instance.enemyCam_GO.GetComponent<Image>().sprite = enemySprite_List[kills]; //New enemy sprite
            //Health
            enemyHPSlider.SetMaxHealth(kills+1);
            enemyHealth = kills+1;
            enemyHPSlider.SetHealth(kills+1);
        }

//**********START GAME**********// 
        public void StartFight(){
            AudioManager.Instance.StopMenuMusic(); //Stop playing menu music
            AudioManager.Instance.StartIngameMusic(); //Start playing game music
            //Set player health
            playerHPSlider.SetMaxHealth(3);
            playerHealth = 3;
            playerHPSlider.SetHealth(3);
            //Set kills to 0                
            kills = 0;
            GameReset();
            CreateEnemy(streak); //Create enemy
        }

//**********OTRAS FUNCIONALIDADES**********//
//**********QUIT**********//
        public void QuitGame(){
            Application.Quit();
        }

//**********OPEN/CLOSE SETTINGS**********//
        public void OpenSettings(){
            ScreenManager.Instance.AddScreen(GameScreens.Settings);;
        }
        public void CloseSettings(){
            ScreenManager.Instance.RemoveScreen(GameScreens.Settings);
        }
    }
}
