using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsDataStorage
{
    public List<LevelDataSerializable> levels;

    public LevelsDataStorage()
    {
        levels = new List<LevelDataSerializable>();
    }

    public void AddOrUpdateLevel(LevelDataSerializable level)
    {
        if (levels == null)
        {
            levels = new List<LevelDataSerializable>();
        }

        LevelDataSerializable existingLevel = levels.Find(l => l.currentLevel == level.currentLevel);
        if (existingLevel != null)
        {
            
            // Update the existing level's best time if the new time is better
            if (level.bestTime < existingLevel.bestTime || existingLevel.bestTime == 0)
            {
                existingLevel.bestTime = level.bestTime;
                existingLevel.isUnlocked = true;
            }
            //Debug.Log(level.isUnlocked);
        }
        else
        {
            levels.Add(level);
        }
    }

    public LevelDataSerializable GetLevelInfo(int levelIndex)
    {
        if (levels == null)
        {
            return null;
        }
        return levels.Find(l => l.currentLevel == levelIndex);
    }

    public void fillEmptyLevels(){
        for(int i = 0; i < 10; i++){
            LevelDataSerializable level = GetLevelInfo(i);
            if (level == null)
            {
                level = new LevelDataSerializable(i, 0, false, 0);
                AddOrUpdateLevel(level);
            }
        }
    }
}
