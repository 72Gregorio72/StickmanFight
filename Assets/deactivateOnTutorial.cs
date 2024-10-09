using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateOnTutorial : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelLists.choosedLevel == 0)
        {
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        } else {
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
