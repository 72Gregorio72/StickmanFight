using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using Microsoft.Unity.VisualStudio.Editor;

public class textChanger : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI medalsTime;
    private TextMeshProUGUI devTimeText;

    private TextMeshProUGUI attemptText;

    private TextMeshProUGUI coinText;
    private LevelInfo level;
    private SpriteRenderer bg;
    private GameObject medal;

    public Sprite[] medals;

    private GameObject LockLevel;

    // Start is called before the first frame update
    void Start()
    {
        levelText = GameObject.FindGameObjectWithTag("LevelText")?.GetComponent<TextMeshProUGUI>();
        timeText = GameObject.FindGameObjectWithTag("TimeText")?.GetComponent<TextMeshProUGUI>();
        medalsTime = GameObject.FindGameObjectWithTag("MedalsTime")?.GetComponent<TextMeshProUGUI>();
        devTimeText = GameObject.FindGameObjectWithTag("DevTime")?.GetComponent<TextMeshProUGUI>();
        medal = GameObject.FindGameObjectWithTag("CongratsMedal");
        bg = GameObject.FindGameObjectWithTag("BG")?.GetComponent<SpriteRenderer>();
        LockLevel = GameObject.FindGameObjectWithTag("Lock");
        attemptText = GameObject.FindGameObjectWithTag("AttemptText")?.GetComponent<TextMeshProUGUI>();
        coinText = GameObject.FindGameObjectWithTag("CoinText")?.GetComponent<TextMeshProUGUI>();


        LevelLists.choosedLevel = 0;

        changeText();
    }

    public void changeText()
    {
        CoinManager.LoadCoins();

        if (LevelLists.choosedLevel < 0 || LevelLists.choosedLevel >= LevelLists.levels.Count)
        {
            Debug.LogError("Invalid choosedLevel index.");
            return;
        }

        level = LevelLists.levels[LevelLists.choosedLevel].GetComponent<LevelInfo>();
        if (level == null)
        {
            return;
        }

        if (LevelLists.choosedLevel == 0)
        {
            levelText.text = "Tutorial";
            timeText.text = "";
            medalsTime.text = "";
            devTimeText.text = "";
            attemptText.text = "";
            coinText.text = "Coins: " + CoinManager.GetCoins().ToString();

            medal.SetActive(false);
            LockLevel.SetActive(false); 
        }
        else
        {
            medal.SetActive(true); 
            levelText.text = (LevelLists.choosedLevel).ToString();
            LevelsDataStorage loadedData = saveSystem.LoadLevelsData();
            attemptText.text = "" + loadedData.levels[LevelLists.choosedLevel].attempt;
            coinText.text = "Coins: " + CoinManager.GetCoins().ToString();
            if (loadedData == null)
            {
                Debug.LogError("Failed to load level data.");
                return;
            }

            LevelDataSerializable levelData = loadedData.GetLevelInfo(LevelLists.choosedLevel - 1);
            if (levelData == null)
            {
                timeText.text = "_ _ _";
            }
            else
            {
                timeText.text = levelData.bestTime.ToString("0.00");
            }

            medalsTime.text = $"{level.goldTime:0.00}\n\n{level.silverTime:0.00}\n\n<ANY>";
            devTimeText.text = level.DevTimer.ToString("0.00");

            int medalIndex = 0;
            if (levelData != null)
            {
                if(levelData.bestTime != 0){
                    if (Mathf.Round(levelData.bestTime * 100) / 100 <= Mathf.Round(level.DevTimer * 100) / 100)
                    {
                        medalIndex = 0; // dev medal
                    }
                    else if (Mathf.Round(levelData.bestTime * 100) / 100 <= Mathf.Round(level.goldTime * 100) / 100)
                    {
                        medalIndex = 1; // gold medal
                    }
                    else if(Mathf.Round(levelData.bestTime * 100) / 100 <= Mathf.Round(level.silverTime * 100) / 100)
                    {
                        medalIndex = 2; // silver medal
                    }
                    else if(levelData.bestTime != 0)
                    {
                        medalIndex = 3; // bronze medal
                    }
                } else {
                    medalIndex = 4; // no medal
                }
                
                if(levelData.isUnlocked){
                    LockLevel.SetActive(false);
                } else {
                    LockLevel.SetActive(true);
                }
            } else {
                LockLevel.SetActive(false);
            }

            medal.GetComponent<SpriteRenderer>().sprite = medals[medalIndex];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
