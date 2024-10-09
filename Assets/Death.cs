using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public Animator anim;
    private bool died = false;
    private GameObject levelContainer;
    // Start is called before the first frame update

    Sounds sounds;
    void Start()
    {
        levelContainer = GameObject.FindGameObjectWithTag("LevelContainer");
        sounds = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && died){
            restartGame();
        }
    }

    public void Die(){
        anim.SetTrigger("Death");
        anim.SetBool("jumping", false);
        anim.SetBool("wallSliding", false);
        sounds.PlayDie();
        // Disable the PlayerMovement script
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        playerMovement.horizontalMovement = 0;
        playerMovement.rb.velocity = new Vector3(0, 0, 0);
        died = true;
}

    public void restartGame(){
        LevelInfo levelInfo = levelContainer.GetComponent<LevelInfo>();
        if(levelInfo.nextLevel() != null)
        {
                LevelData.attempt++;
                LevelsDataStorage levelsData = saveSystem.LoadLevelsData();
                levelInfo.attempt++;
                // Crea o aggiorna il livello con il nuovo best time
                LevelDataSerializable updatedLevel = new LevelDataSerializable(levelInfo.currentLevel - 1, levelsData.levels[levelInfo.currentLevel].bestTime, true, LevelData.attempt);                    
                levelsData.AddOrUpdateLevel(updatedLevel);

                levelsData.levels[levelInfo.currentLevel].attempt++;

                // Salva i dati aggiornati
                saveSystem.SaveLevelsData(levelsData);
                //Debug.Log(levelsData.levels[levelInfo.currentLevel].attempt);
        }
        SceneManager.LoadScene("Level" + levelContainer.GetComponent<LevelInfo>().currentLevel);
    }
}
