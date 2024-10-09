using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCheck : MonoBehaviour
{
    private GameObject levelContainer;
    private CoinContainer coins;
    public Animator anim;
    Sounds sounds;
    private TimerScript ts;

    // Start is called before the first frame update
    void Start()
    {
        levelContainer = GameObject.FindGameObjectWithTag("LevelContainer");
        coins = GameObject.FindGameObjectWithTag("CoinContainer").GetComponent<CoinContainer>();   
        sounds = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Sounds>(); 
        ts = GameObject.FindGameObjectWithTag("TimerContainer").GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(coins.currentCoins >= coins.maxCoins){
            anim.SetBool("DoorOpen", true);
            sounds.PlayDoorOpening();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            LevelInfo levelInfo = levelContainer.GetComponent<LevelInfo>();
            if(levelInfo.nextLevel() != null)
            {
                if(coins.currentCoins >= coins.maxCoins)
                {
                    ts.StopTimer();
                    LevelData.nextLevel = levelInfo.currentLevel + 1;
                    LevelData.finishedTime = ts.finishedTime;
                    LevelData.goldTime = levelInfo.goldTime;
                    LevelData.silverTime = levelInfo.silverTime;
                    LevelData.bronzeTime = levelInfo.bronzeTime;
                    LevelData.DevTimer = levelInfo.DevTimer;

                    LevelData.attempt++;
                    LevelsDataStorage levelsData = saveSystem.LoadLevelsData();
                    levelInfo.attempt++;

                    // Crea o aggiorna il livello con il nuovo best time
                    LevelDataSerializable updatedLevel = new LevelDataSerializable(levelInfo.currentLevel - 1, ts.finishedTime, true, LevelData.attempt);                    
                    levelsData.AddOrUpdateLevel(updatedLevel);

                    levelsData.levels[levelInfo.currentLevel].isUnlocked = true;

                    levelsData.levels[0].isUnlocked = true;

                    //Debug.Log(updatedLevel.isUnlocked);

                    levelsData.levels[levelInfo.currentLevel].attempt++;

                    CoinManager.LoadCoins();
                    
                    CoinManager.AddCoins(1);

                    // Salva i dati aggiornati
                    saveSystem.SaveLevelsData(levelsData);

                    SceneManager.LoadScene("FinishedLevel");
                }
                else
                {
                    Debug.Log("You need to collect all the coins!");
                }
            }
            else
            {
                LevelData.nextLevel = -1;
                LevelData.finishedTime = ts.finishedTime;
                LevelData.goldTime = levelInfo.goldTime;
                LevelData.silverTime = levelInfo.silverTime;
                LevelData.bronzeTime = levelInfo.bronzeTime;
                LevelData.DevTimer = levelInfo.DevTimer;

                SceneManager.LoadScene("FinishedLevel");
            }
        }
    }
}
