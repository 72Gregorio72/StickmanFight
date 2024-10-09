using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinContainer : MonoBehaviour
{
    public int currentCoins;

    public int maxCoins;
    private TextMeshProUGUI coinText;

    private List<GameObject> coins;

    // Start is called before the first frame update
    void Start()
    {
        currentCoins = 0;

        coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));

        coinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<TextMeshProUGUI>();

        maxCoins = coins.Count;

        coinText.text = ":" + currentCoins.ToString() + "/" + maxCoins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCoin(){
        currentCoins++;
        coinText.text = ":" + currentCoins.ToString() + "/" + maxCoins;
    }
}
