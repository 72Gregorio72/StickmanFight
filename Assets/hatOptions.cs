using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatOptions : MonoBehaviour
{
    public List<GameObject> options = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        HatList.hats.Clear();
        foreach (GameObject option in options)
        {
            HatList.hats.Add(option);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
