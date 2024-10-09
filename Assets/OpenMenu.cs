using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenMenu : MonoBehaviour
{
    public Canvas canvas;
    private GameObject lastSelectedGameObject;

    public GameObject[] objectsToDeactivate;

    public GameObject[] buttonsToDeactivate;

    // Start is called before the first frame update
    void Start()
    {
        lastSelectedGameObject = null;
        StartCoroutine(DisableCanvasAfterDelay(0.05f));
    }

    private IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        canvas.enabled = true;

        foreach (GameObject obj in buttonsToDeactivate)
        {
            obj.SetActive(true);
        }

        Time.timeScale = 0f;
        lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }

    public void Close()
    {
        Time.timeScale = 1f;
        canvas.enabled = false;
        foreach (GameObject obj in buttonsToDeactivate)
        {
            obj.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
        if (lastSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
        }
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }
    }

    public void RestartLevel(){
        Close();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Death>().restartGame();
    }
}
