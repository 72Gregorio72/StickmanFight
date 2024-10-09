using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChooser : MonoBehaviour
{
    public GameObject player;

    private List<GameObject> options = skinsList.skins;
    // Start is called before the first frame update
    private int currentSkinindex;
    void Start()
    {
        changeSkin(skinsList.optionChoosed);

        currentSkinindex = skinsList.optionChoosed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextSkin(){
        currentSkinindex++;
        if(currentSkinindex >= options.Count){
            currentSkinindex = 0;
        }
        skinsList.optionChoosed = currentSkinindex;
        changeSkin(currentSkinindex);
    }
    
    public void previousSkin(){
        currentSkinindex--;
        if(currentSkinindex < 0){
            currentSkinindex = options.Count - 1;
        }
        skinsList.optionChoosed = currentSkinindex;
        changeSkin(currentSkinindex);
    }

    void changeSkin(int index){
        if(index >= 0 && index < options.Count){
            player.transform.Find("head").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("head").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("body").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("body").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("leftLeg").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("leftLeg").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("rightLeg").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("rightLeg").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("leftFoot").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("leftFoot").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("rightFoot").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("rightFoot").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("leftArm").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("leftArm").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("rightArm").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("rightArm").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("leftHand").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("leftHand").GetComponent<SpriteRenderer>().sprite;
            player.transform.Find("rightHand").GetComponent<SpriteRenderer>().sprite = options[index].transform.Find("rightHand").GetComponent<SpriteRenderer>().sprite;
        }
    }
}
