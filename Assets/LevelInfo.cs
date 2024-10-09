using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelInfo : MonoBehaviour
{
    public string levelName;
    public string levelDescription;
    public int currentLevel;
    public float goldTime;
    public float silverTime;
    public float bronzeTime;
    public float DevTimer;
    public float bestTime = 0;

    public int attempt;
    public Sprite backGround;
    public Material levelPreview;

    public LevelInfo(int currentLevel, float bestTime)
    {
        this.currentLevel = currentLevel;
        this.bestTime = bestTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelsDataStorage data = saveSystem.LoadLevelsData();
        if (data != null)
        {
            /*LevelInfo levelInfo = data.GetLevelInfo(currentLevel);
            if (levelInfo != null)
            {
                bestTime = levelInfo.bestTime;
            }*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public string nextLevel()
    {
        string nextLevelName = "Level" + (currentLevel + 1);
        // Verifica se la scena è presente nei Build Settings
        if (IsSceneInBuildSettings(nextLevelName))
        {
            return nextLevelName;
        }

        return null;
    }

    public void IncrementAttempt(LevelInfo levelInfo)
    {
        LevelsDataStorage levelsData = saveSystem.LoadLevelsData();
        levelInfo.attempt++;
        LevelDataSerializable updatedLevel = new LevelDataSerializable(levelInfo.currentLevel - 1, levelInfo.bestTime, true, levelInfo.attempt);                    
        levelsData.AddOrUpdateLevel(updatedLevel);

        // Salva i dati aggiornati
        saveSystem.SaveLevelsData(levelsData);
    }

    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuild = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameInBuild == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
