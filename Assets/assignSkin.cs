using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assignSkin : MonoBehaviour
{
    public GameObject player;

    private GameObject skins;

    // Start is called before the first frame update
    void Start()
    {
        if(skinsList.optionChoosed >= 0){
            if(skinsList.skins[skinsList.optionChoosed] != null){
                skins = skinsList.skins[skinsList.optionChoosed];
            } else {
                skins = skinsList.skins[0];
            }
            changeSkin();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void changeSkin(){
        player.transform.Find("head").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("head").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("body").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("body").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("leftLeg").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("leftLeg").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("rightLeg").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("rightLeg").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("leftFoot").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("leftFoot").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("rightFoot").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("rightFoot").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("leftArm").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("leftArm").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("rightArm").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("rightArm").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("leftHand").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("leftHand").GetComponent<SpriteRenderer>().sprite;
        player.transform.Find("rightHand").GetComponent<SpriteRenderer>().sprite = skins.transform.Find("rightHand").GetComponent<SpriteRenderer>().sprite;
    }
}
