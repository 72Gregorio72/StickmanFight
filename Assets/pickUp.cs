using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    private GameObject CoinContainer;

    Sounds sounds;
    // Start is called before the first frame update
    void Start()
    {
        CoinContainer = GameObject.FindGameObjectWithTag("CoinContainer");

        sounds = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinContainer.GetComponent<CoinContainer>().addCoin();
            sounds.PlayKeyPickUp();
            Destroy(gameObject);
        }
    }
}
