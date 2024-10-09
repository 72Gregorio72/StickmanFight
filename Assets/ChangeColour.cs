using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    GameObject color;
    // Start is called before the first frame update
    void Start()
    {
        color = GameObject.FindGameObjectWithTag("ColorContainer");
        GetComponent<SpriteRenderer>().material.color = color.GetComponent<Colours>().getBlockColour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
