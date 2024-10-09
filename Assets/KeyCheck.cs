using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject resumeButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) && resumeButton.active == false){
            pauseMenu.GetComponent<OpenMenu>().Open();
        } else if(Input.GetKeyDown(KeyCode.Escape) && resumeButton.active == true){
            pauseMenu.GetComponent<OpenMenu>().Close();
        }
    }
}
