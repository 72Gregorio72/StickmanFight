using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinOptions : MonoBehaviour
{
    public List<GameObject> options = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        skinsList.skins.Clear();
        foreach (GameObject option in options)
        {
            skinsList.skins.Add(option);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
