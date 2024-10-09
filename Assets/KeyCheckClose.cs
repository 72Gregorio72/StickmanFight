using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheckClose : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.GetComponent<OpenMenu>().Close();
        }
    }
}
