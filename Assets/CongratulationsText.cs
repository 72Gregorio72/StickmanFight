using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CongratulationsText : MonoBehaviour
{
    private TextMeshProUGUI NextLevelText;
    private TextMeshProUGUI time;

    private TextMeshProUGUI bestTime;
    // Start is called before the first frame update
    void Start()
    {
        NextLevelText = GameObject.FindGameObjectWithTag("NextLevelText").GetComponent<TextMeshProUGUI>();
        time = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        bestTime = GameObject.FindGameObjectWithTag("bestTime").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        NextLevelText.text = "Level" + LevelData.nextLevel;
        time.text = "" + LevelData.finishedTime.ToString("F2");
        LevelsDataStorage loadedData = saveSystem.LoadLevelsData();
        LevelDataSerializable levelData = loadedData.GetLevelInfo(LevelData.nextLevel - 2);

        bestTime.text = "Best:\n\n" + Mathf.Round(levelData.bestTime * 100) / 100;
    }

    public void NextLevel()
    {
        //if(NextLevelText.text != "Level-1"){
            SceneManager.LoadScene(NextLevelText.text);
        //}
    }

    public void RestartLevel()
    {
        int currentLevel = LevelData.nextLevel - 1;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
