using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConteiner : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();

    private int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject level in levels)
        {
            
            LevelLists.levels.Add(level);
        }
        //GameObject.FindGameObjectsWithTag("LevelContainer")[0].GetComponent<textChanger>().inizializeText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextLevel(){
        if (currentLevel < levels.Count - 1)
        {
            currentLevel++;
        } else {
            currentLevel = 0;
        }

        LevelLists.choosedLevel = currentLevel;

        GameObject.FindGameObjectsWithTag("LevelContainer")[0].GetComponent<textChanger>().changeText();
    }

    public void previousLevel(){
        if (currentLevel > 0)
        {
            currentLevel--;
        } else {
            currentLevel = levels.Count - 1;
        }

        LevelLists.choosedLevel = currentLevel;

        GameObject.FindGameObjectsWithTag("LevelContainer")[0].GetComponent<textChanger>().changeText();
    }
}
