using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colours : MonoBehaviour
{
    public Color blockColor = Color.red;
    public Color bgColor = Color.yellow;
    public Color getBlockColour(){
        return blockColor;
    }

    public Color getBgColour(){
        return bgColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.backgroundColor = bgColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
