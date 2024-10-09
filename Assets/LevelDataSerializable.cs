[System.Serializable]
public class LevelDataSerializable
{
    public int currentLevel;
    public float bestTime;
    public int attempt;
    public bool isUnlocked;

    public LevelDataSerializable(int currentLevel, float bestTime, bool isUnlocked, int attempt)
    {
        this.currentLevel = currentLevel;
        this.bestTime = bestTime;
        this.isUnlocked = isUnlocked;
        this.attempt = attempt;
    }

    public LevelDataSerializable(int attempt)
    {
        this.attempt = attempt;
        this.isUnlocked = true;
    }
}
