using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class changeTilemap : MonoBehaviour
{
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Background").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
