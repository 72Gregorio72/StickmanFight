using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assignHat : MonoBehaviour
{
    public GameObject player;

    private GameObject hat;

    // Start is called before the first frame update
    void Start()
    {
        if(HatList.optionChoosed >= 0 && HatList.optionChoosed < HatList.hats.Count && HatList.hats[HatList.optionChoosed] != null){
            hat = HatList.hats[HatList.optionChoosed];
        } else {
            hat = HatList.hats[0];
        }
        
        changeSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeSkin(){
        player.GetComponent<SpriteRenderer>().sprite = hat.GetComponent<SpriteRenderer>().sprite;
    }
}
