using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static int coins;

    void Awake()
    {
        LoadCoins();
    }

    public static void AddCoins(int amount)
    {
        coins += amount;
        SaveCoins();
    }

    public static void SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            SaveCoins();
        }
        else
        {
            Debug.Log("Non hai abbastanza monete.");
        }
    }

    public static int GetCoins()
    {
        return coins;
    }

    private static void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public static void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void OnApplicationQuit()
    {
        SaveCoins();
    }
}
