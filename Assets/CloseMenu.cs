using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    public GameObject menuUI; // Assign in the inspector
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenuUI()
    {
        menuUI.SetActive(false);
    }

    public void OpenMenuUI()
    {
        menuUI.SetActive(true);
    }
}
