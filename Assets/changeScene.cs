using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSceneTo(string scene){
        SceneManager.LoadScene(scene);
    }

    public void PlayLevel(){
        LevelsDataStorage levelsData = saveSystem.LoadLevelsData();
        LevelInfo level = LevelLists.levels[LevelLists.choosedLevel].GetComponent<LevelInfo>();

        if(level.currentLevel > 0){
            if(levelsData.levels[level.currentLevel - 1].isUnlocked){
                SceneManager.LoadScene("Level" + level.currentLevel);
            } else {
                Debug.Log("Level is locked");
            }
        } else {
            SceneManager.LoadScene("Level0");
        }
    }
}
