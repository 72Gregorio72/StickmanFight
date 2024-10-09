using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAssign : MonoBehaviour
{
    private TextMeshProUGUI levelText;

    private TextMeshProUGUI medalsTime;

    private TextMeshProUGUI devTime;

    private GameObject levelInfo;

    // Start is called before the first frame update
    void Start()
    {
        levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<TextMeshProUGUI>();
        medalsTime = GameObject.FindGameObjectWithTag("MedalsTime").GetComponent<TextMeshProUGUI>();
        devTime = GameObject.FindGameObjectWithTag("DevTime").GetComponent<TextMeshProUGUI>();
        levelInfo = GameObject.FindGameObjectWithTag("LevelContainer");
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "" + GameObject.FindGameObjectWithTag("LevelContainer").GetComponent<LevelInfo>().currentLevel;
        medalsTime.text = GameObject.FindGameObjectWithTag("LevelContainer").GetComponent<LevelInfo>().goldTime + ".00\n\n\n" + levelInfo.GetComponent<LevelInfo>().silverTime + ".00\n\n\n<ANY>";
        devTime.text = "" + GameObject.FindGameObjectWithTag("LevelContainer").GetComponent<LevelInfo>().DevTimer;
    }
}
