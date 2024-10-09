using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatChooser : MonoBehaviour
{
    public GameObject player;

    private List<GameObject> options = HatList.hats;
    // Start is called before the first frame update
    private int currentSkinindex;
    void Start()
    {
        changeSkin(HatList.optionChoosed);

        currentSkinindex = HatList.optionChoosed;
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
        HatList.optionChoosed = currentSkinindex;
        changeSkin(currentSkinindex);
    }
    
    public void previousSkin(){
        currentSkinindex--;
        if(currentSkinindex < 0){
            currentSkinindex = options.Count - 1;
        }
        HatList.optionChoosed = currentSkinindex;
        changeSkin(currentSkinindex);
    }

    void changeSkin(int index){
        if(index >= 0 && index < options.Count){
            player.GetComponent<SpriteRenderer>().sprite = options[index].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
