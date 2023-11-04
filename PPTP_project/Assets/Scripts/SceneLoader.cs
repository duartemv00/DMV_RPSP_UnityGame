using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    int sceneIndex = 1;
    public Slider loadSlider;


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

    void Start(){
        LoadLevel(sceneIndex);
    }

    public void LoadLevel (int sceneIndex){
        StartCoroutine(LoadAsynchronously (sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex){

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone){
            float progress =Mathf.Clamp01(operation.progress/0.9f);
            Debug.Log(operation.progress);
            loadSlider.value = progress;
            yield return null;
        }
    }
}
