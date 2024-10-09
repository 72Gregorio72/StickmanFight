using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float time = 0;
    private bool timerActive = false;
    public bool timerEnded = false;

    public float finishedTime;

    private TextMeshProUGUI timerText; 
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            timerActive = true;
        }

        if(timerActive){
            time += Time.deltaTime;
            timerText.text = "Time: " + time.ToString("F2");
        }
    }

    public void StopTimer(){
        timerActive = false;
        finishedTime = time;
        timerEnded = true;
    }
}
